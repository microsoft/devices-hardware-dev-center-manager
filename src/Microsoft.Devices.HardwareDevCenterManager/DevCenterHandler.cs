/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.  
--*/

using Microsoft.Devices.HardwareDevCenterManager.Utility;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class DevCenterHandler : IDisposable, IDevCenterHandler
{
    private readonly DelegatingHandler _authHandler;
    private readonly AuthorizationHandlerCredentials _authCredentials;
    private readonly TimeSpan _httpTimeout;
    private Guid _correlationId;
    private readonly LastCommandDelegate _lastCommand;
    private DevCenterTrace _trace;

    /// <summary>
    /// Creates a new DevCenterHandler using the provided credentials
    /// </summary>
    /// <param name="credentials">Authorization credentials for HDC</param>
    /// <param name="options">Options object containing options necessary for DevCenterHandler to function</param>
    public DevCenterHandler(AuthorizationHandlerCredentials credentials, DevCenterOptions options)
    {
        _authCredentials = credentials;
        _authHandler = new AuthorizationHandler(_authCredentials, options.HttpTimeoutSeconds);
        _httpTimeout = TimeSpan.FromSeconds(options.HttpTimeoutSeconds);
        _correlationId = options.CorrelationId;
        _lastCommand = options.LastCommand;
    }

    private string GetDevCenterBaseUrl()
    {
        Uri returnUri = _authCredentials.Url;
        if (_authCredentials.UrlPrefix != null)
        {
            returnUri = new Uri(returnUri, _authCredentials.UrlPrefix);
        }
        return returnUri.AbsoluteUri;
    }

    private const string _defaultErrorCode = "InvalidInput";

    /// <summary>
    /// Invokes HDC service with the specified options
    /// </summary>
    /// <param name="method">HTTP method to use</param>
    /// <param name="uri">URI of the service to invoke</param>
    /// <param name="input">Content used as the request options</param>
    /// <param name="processContent">Process the service return content</param>
    /// <returns>Dev Center response with either an error or null if the operation was successful</returns>
    public async Task<DevCenterErrorDetails> InvokeHdcService(
        HttpMethod method, string uri, object input, Action<string> processContent)
    {
        DevCenterErrorReturn returnError = null;
        string RequestId = Guid.NewGuid().ToString();
        string json = JsonSerializer.Serialize(input ?? new object());

        using HttpClient client = new(_authHandler, false);
        client.DefaultRequestHeaders.Add("MS-CorrelationId", _correlationId.ToString());
        client.DefaultRequestHeaders.Add("MS-RequestId", RequestId);

        _trace = new DevCenterTrace()
        {
            CorrelationId = _correlationId.ToString(),
            RequestId = RequestId,
            Method = method.ToString(),
            Url = uri,
            Content = json
        };

        _lastCommand?.Invoke(new DevCenterErrorDetails()
        {
            Trace = _trace
        });

        client.Timeout = _httpTimeout;
        Uri restApi = new(uri);

        if (!(HttpMethod.Get == method || HttpMethod.Post == method || HttpMethod.Put == method))
        {
            return new DevCenterErrorDetails
            {
                HttpErrorCode = -1,
                Code = _defaultErrorCode,
                Message = "Unsupported HTTP method",
                Trace = _trace
            };
        }

        HttpResponseMessage infoResult = null;

        try
        {
            if (HttpMethod.Get == method)
            {
                infoResult = await client.GetAsync(restApi);
            }
            else if (HttpMethod.Post == method)
            {
                StringContent postContent = new(json, System.Text.Encoding.UTF8, "application/json");
                infoResult = await client.PostAsync(restApi, postContent);
            }
            else if (HttpMethod.Put == method)
            {
                infoResult = await client.PutAsync(restApi, null);
            }
        }
        catch (TaskCanceledException tcex)
        {
            if (!tcex.CancellationToken.IsCancellationRequested)
            {
                // HDC time out, wait a bit and try again
                Thread.Sleep(2000);
            }
            else
            {
                throw tcex;
            }
        }

        string content = await infoResult.Content.ReadAsStringAsync();

        if (infoResult.IsSuccessStatusCode)
        {
            processContent?.Invoke(content);
            return null;
        }

        try
        {
            returnError = JsonSerializer.Deserialize<DevCenterErrorReturn>(content);
        }
        catch (JsonException)
        {
            // Error is in bad format, return raw
            returnError = new DevCenterErrorReturn()
            {
                HttpErrorCode = (int)infoResult.StatusCode,
                StatusCode = infoResult.StatusCode.ToString("D") + " " + infoResult.StatusCode.ToString(),
                Message = content
            };
        }

        // returnError can be null when there is HTTP error
        if (returnError == null || (returnError.HttpErrorCode.HasValue && returnError.HttpErrorCode.Value == 0))
        {
            returnError = new DevCenterErrorReturn()
            {
                HttpErrorCode = (int)infoResult.StatusCode,
                StatusCode = infoResult.StatusCode.ToString("D") + " " + infoResult.StatusCode.ToString(),
                Message = infoResult.ReasonPhrase
            };
        }

        if (returnError.Error != null)
        {
            // include additional error details if missing from deserialization
            returnError.Error.HttpErrorCode = (int)infoResult.StatusCode;

            return returnError.Error;
        }

        return new DevCenterErrorDetails
        {
            Headers = infoResult.Headers,
            HttpErrorCode = (int)infoResult.StatusCode,
            Code = returnError.StatusCode,
            Message = returnError.Message,
            ValidationErrors = returnError.ValidationErrors,
            Trace = _trace
        };
    }

    /// <summary>
    /// Invokes HDC service with the specified options
    /// </summary>
    /// <param name="method">HTTP method to use</param>
    /// <param name="uri">URI of the service to invoke</param>
    /// <param name="input">Options for the new artifact to be generated</param>
    /// <param name="isMany">Whether the result has a single entity or multiple</param>
    /// <returns>Dev Center response with either an error or an artifact if created/queried successfully</returns>
    public async Task<DevCenterResponse<Output>> InvokeHdcService<Output>(
        HttpMethod method, string uri, object input, bool isMany) where Output : IArtifact
    {
        DevCenterResponse<Output> devCenterResponse = new();
        devCenterResponse.Error = await InvokeHdcService(method, uri, input, (content) =>
        {
            if (isMany)
            {
                Response<Output> response = JsonSerializer.Deserialize<Response<Output>>(content);
                devCenterResponse.ReturnValue = response.Value;
            }
            else
            {
                Output ret = JsonSerializer.Deserialize<Output>(content);
                if (ret.Id != null)
                {
                    devCenterResponse.ReturnValue = new List<Output> { ret };
                }
            }
        });

        devCenterResponse.Trace = _trace;
        return devCenterResponse;
    }

    /// <summary>
    /// Invokes HDC GET request with the specified options
    /// </summary>
    /// <param name="uri">URI of the service to invoke</param>
    /// <param name="isMany">Whether the result has a single entity or multiple</param>
    /// <returns>Dev Center response with either an error or a list of artifacts if queried successfully</returns>
    public async Task<DevCenterResponse<Output>> HdcGet<Output>(string uri, bool isMany) where Output : IArtifact
    {
        return await InvokeHdcService<Output>(HttpMethod.Get, uri, null, isMany);
    }

    /// <summary>
    /// Invokes HDC POST request with the specified options
    /// </summary>
    /// <param name="uri">URI of the service to invoke</param>
    /// <param name="input">Options for the new artifact to be generated</param>
    /// <returns>Dev Center response with either an error or an artifact if created successfully</returns>
    public async Task<DevCenterResponse<Output>> HdcPost<Output>(string uri, object input) where Output : IArtifact
    {
        return await InvokeHdcService<Output>(HttpMethod.Post, uri, input, false);
    }

    private const string _devCenterProductsUrl = "/hardware/products";

    /// <summary>
    /// Creates a new New Product in HDC with the specified options
    /// </summary>
    /// <param name="input">Options for the new Product to be generated</param>
    /// <returns>Dev Center response with either an error or a Product if created successfully</returns>
    public async Task<DevCenterResponse<Product>> NewProduct(NewProduct input)
    {
        string newProductsUrl = GetDevCenterBaseUrl() + _devCenterProductsUrl;
        return await HdcPost<Product>(newProductsUrl, input);
    }

    /// <summary>
    /// Gets a list of products or a specific product from HDC
    /// </summary>
    /// <param name="productId">Gets all products if null otherwise retrieves the specified product</param>
    /// <returns>Dev Center response with either an error or a Product if queried successfully</returns>
    public async Task<DevCenterResponse<Product>> GetProducts(string productId = null)
    {
        string getProductsUrl = GetDevCenterBaseUrl() + _devCenterProductsUrl;

        bool isMany = string.IsNullOrEmpty(productId);
        if (!isMany)
        {
            getProductsUrl += "/" + Uri.EscapeDataString(productId);
        }

        return await HdcGet<Product>(getProductsUrl, isMany);
    }

    private const string _devCenterProductSubmissionUrl = "/hardware/products/{0}/submissions";

    /// <summary>
    /// Creates a new Submission in HDC with the specified options
    /// </summary>
    /// <param name="productId">Specify the Product ID for this Submission</param>
    /// <param name="submissionInfo">Options for the new Submission to be generated</param>
    /// <returns>Dev Center response with either an error or a Submission if created successfully</returns>
    public async Task<DevCenterResponse<Submission>> NewSubmission(string productId, NewSubmission submissionInfo)
    {
        string newProductSubmissionUrl = GetDevCenterBaseUrl() +
            string.Format(_devCenterProductSubmissionUrl, Uri.EscapeDataString(productId));
        return await HdcPost<Submission>(newProductSubmissionUrl, submissionInfo);
    }

    /// <summary>
    /// Gets a list of submissions or a specific submission from HDC
    /// </summary>
    /// <param name="productId">Specify the Product ID for this Submission</param>
    /// <param name="submissionId">Gets all submissions if null otherwise retrieves the specified submission</param>
    /// <returns>Dev Center response with either an error or a Submission if queried successfully</returns>
    public async Task<DevCenterResponse<Submission>> GetSubmission(string productId, string submissionId = null)
    {
        string getProductSubmissionUrl = GetDevCenterBaseUrl() +
            string.Format(_devCenterProductSubmissionUrl, Uri.EscapeDataString(productId));

        bool isMany = string.IsNullOrEmpty(submissionId);
        if (!isMany)
        {
            getProductSubmissionUrl += "/" + Uri.EscapeDataString(submissionId);
        }

        return await HdcGet<Submission>(getProductSubmissionUrl, isMany);
    }

    private const string _devCenterPartnerSubmissionUrl =
        "/hardware/products/relationships/sourcepubliherid/{0}/sourceproductid/{1}/sourcesubmissionid/{2}";

    /// <summary>
    /// Gets shared submission info from a partner-shared Submission with partner ids
    /// </summary>
    /// <param name="publisherId">Specify the Partner's Publisher ID for this Submission</param>
    /// <param name="productId">Specify the Partner's Product ID for this Submission</param>
    /// <param name="submissionId">Specify the Partner's Submission ID for this Submission</param>
    /// <returns>Dev Center response with either an error or a Submission if queried successfully with IDs for the querying account</returns>
    public async Task<DevCenterResponse<Submission>> GetPartnerSubmission(
        string publisherId, string productId, string submissionId)
    {
        string getProductSubmissionUrl = GetDevCenterBaseUrl() +
            string.Format(_devCenterPartnerSubmissionUrl, Uri.EscapeDataString(publisherId),
            Uri.EscapeDataString(productId), Uri.EscapeDataString(submissionId));
        return await HdcGet<Submission>(getProductSubmissionUrl, string.IsNullOrEmpty(submissionId));
    }

    private const string _devCenterProductSubmissionCommitUrl = "/hardware/products/{0}/submissions/{1}/commit";
    /// <summary>
    /// Commits a Submission in HDC
    /// </summary>
    /// <param name="productId">Specify the Product ID for the Submission to commit</param>
    /// <param name="submissionId">Specify the Submission ID for the Submission to commit</param>
    /// <returns>Dev Center response with either an error or a true if committed successfully</returns>
    public async Task<DevCenterResponse<bool>> CommitSubmission(string productId, string submissionId)
    {
        string commitProductSubmissionUrl = GetDevCenterBaseUrl() +
            string.Format(_devCenterProductSubmissionCommitUrl, Uri.EscapeDataString(productId), Uri.EscapeDataString(submissionId));
        DevCenterErrorDetails error = await InvokeHdcService(HttpMethod.Post, commitProductSubmissionUrl, null, null);
        DevCenterResponse<bool> ret = new()
        {
            Error = error,
            ReturnValue = new List<bool>()
            {
                error == null
            },
            Trace = _trace
        };

        if (error != null)
        {
            if ((error.HttpErrorCode.HasValue) &&
                (error.HttpErrorCode.Value == (int)System.Net.HttpStatusCode.BadGateway) &&
                (string.Compare(error.Code, "requestInvalidForCurrentState", true) == 0)
                )
            {
                //  Communication issue likely caused the submission to already be done.  Check.
                DevCenterResponse<Submission> SubmissionStatus = await GetSubmission(productId, submissionId);
                if (SubmissionStatus.Error == null)
                {
                    Submission s = SubmissionStatus.ReturnValue[0];
                    if (string.Compare(s.CommitStatus, "commitComplete", true) == 0)
                    {
                        //Actually did commit
                        ret.Error = null;
                        ret.ReturnValue = new List<bool>() { true };
                    }
                }
            }
        }

        return ret;
    }

    private const string _devCenterShippingLabelUrl = "/hardware/products/{0}/submissions/{1}/shippingLabels";

    /// <summary>
    /// Creates a new Shipping Label in HDC with the specified options
    /// </summary>
    /// <param name="productId">Specify the Product ID for this Shipping Label</param>
    /// <param name="submissionId">Specify the Submission ID for this Shipping Label</param>
    /// <param name="shippingLabelInfo">Options for the new Shipping Label to be generated</param>
    /// <returns>Dev Center response with either an error or a ShippingLabel if created successfully</returns>
    public async Task<DevCenterResponse<ShippingLabel>> NewShippingLabel(
        string productId, string submissionId, NewShippingLabel shippingLabelInfo)
    {
        string shippingLabelUrl = GetDevCenterBaseUrl() +
            string.Format(_devCenterShippingLabelUrl, Uri.EscapeDataString(productId), Uri.EscapeDataString(submissionId));
        return await HdcPost<ShippingLabel>(shippingLabelUrl, shippingLabelInfo);
    }

    /// <summary>
    /// Gets a list of shipping labels or a specific shipping label from HDC
    /// </summary>
    /// <param name="productId">Specify the Product ID for this Shipping Label</param>
    /// <param name="submissionId">Specify the Submission ID for this Shipping Label</param>
    /// <param name="shippingLabelId">Gets all Shipping Labels if null otherwise retrieves the specified Shipping Label</param>
    /// <returns>Dev Center response with either an error or a ShippingLabel if queried successfully</returns>
    public async Task<DevCenterResponse<ShippingLabel>> GetShippingLabels(
        string productId, string submissionId, string shippingLabelId = null)
    {
        string getShippingLabelUrl = GetDevCenterBaseUrl() +
            string.Format(_devCenterShippingLabelUrl, Uri.EscapeDataString(productId), Uri.EscapeDataString(submissionId));

        bool isMany = string.IsNullOrEmpty(shippingLabelId);
        if (!isMany)
        {
            getShippingLabelUrl += "/" + Uri.EscapeDataString(shippingLabelId);
        }

        getShippingLabelUrl += "?includeTargetingInfo=true";
        return await HdcGet<ShippingLabel>(getShippingLabelUrl, isMany);
    }

    private const string _devCenterAudienceUrl = "/hardware/audiences";

    /// <summary>
    /// Gets a list of valid audiences from HDC
    /// </summary>
    /// <returns>Dev Center response with either an error or a Audience if queried successfully</returns>
    public async Task<DevCenterResponse<Audience>> GetAudiences()
    {
        string getAudienceUrl = GetDevCenterBaseUrl() + _devCenterAudienceUrl;
        return await HdcGet<Audience>(getAudienceUrl, true);
    }

    private const string _devCenterCreateMetaDataUrl = "/hardware/products/{0}/submissions/{1}/createpublishermetadata";

    /// <summary>
    /// Requests creation of driver metadata on older submissions to HDC
    /// </summary>
    /// <returns>Dev Center response with either an error or ok if metadata was created successfully</returns>
    public async Task<DevCenterResponse<bool>> CreateMetaData(string productId, string submissionId)
    {
        string createMetaDataUrl = GetDevCenterBaseUrl() +
            string.Format(_devCenterCreateMetaDataUrl, Uri.EscapeDataString(productId), Uri.EscapeDataString(submissionId));
        DevCenterErrorDetails error = await InvokeHdcService(HttpMethod.Post, createMetaDataUrl, null, null);
        DevCenterResponse<bool> ret = new()
        {
            Error = error,
            ReturnValue = new List<bool>()
            {
                error == null
            },
            Trace = _trace
        };
        return ret;
    }

    private const string _devCenterCancelShippingLabelUrl = "/hardware/products/{0}/submissions/{1}/shippingLabels/{2}/cancel";

    /// <summary>
    /// Requests cancellation of a shipping label
    /// </summary>
    /// <returns>Dev Center Response with Boolean value indicating a successful call to cancel a the shipping label</returns>
    public async Task<DevCenterResponse<bool>> CancelShippingLabel(string productId, string submissionId, string shippingLabelId)
    {
        string cancelShippingLabelUrl = GetDevCenterBaseUrl() +
            string.Format(_devCenterCancelShippingLabelUrl, productId, submissionId, shippingLabelId);

        DevCenterErrorDetails error = await InvokeHdcService(HttpMethod.Put, cancelShippingLabelUrl, null, null);
        DevCenterResponse<bool> ret = new()
        {
            Error = error,
            ReturnValue = new List<bool>()
            {
                error == null
            },
            Trace = _trace
        };

        return ret;
    }

    public void Dispose()
    {
        _authHandler.Dispose();
    }
}

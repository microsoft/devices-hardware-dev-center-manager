/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using Azure.Core;
using Azure.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Devices.HardwareDevCenterManager.Utility;

internal class HttpRetriesExhaustedException : Exception
{
    public HttpRetriesExhaustedException(string msg) : base(msg) { }
}

internal class AuthorizationHandler : DelegatingHandler
{
    private string _accessToken;
    private readonly AuthorizationHandlerCredentials _authCredentials;
    private readonly TimeSpan _httpTimeout;

    private const int _maxRetries = 10;

    /// <summary>
    /// Handles OAuth Tokens for HTTP request to Microsoft Hardware Dev Center
    /// </summary>
    /// <param name="credentials">The set of credentials to use for the token acquisition</param>
    /// <param name="httpTimeoutSeconds">Integer value specifying HTTP timeout when making requests to HDC</param>
    public AuthorizationHandler(AuthorizationHandlerCredentials credentials, uint httpTimeoutSeconds)
        : base(new HttpClientHandler())
    {
        _accessToken = null;
        _authCredentials = credentials;
        _httpTimeout = TimeSpan.FromSeconds(httpTimeoutSeconds);
    }

    /// <summary>
    /// Inserts Bearer token into HTTP requests and also does a retry on failed requests since
    /// HDC sometimes fails
    /// </summary>
    /// <param name="request">HTTP Request to send</param>
    /// <param name="cancellationToken">CancellationToken in case the request is cancelled</param>
    /// <returns>Returns the HttpResponseMessage from the request</returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        int tries = 0;
        HttpResponseMessage response = null;

        // If there is no valid access token for HDC, get one and then add it to the request
        if (_accessToken == null)
        {
            await ObtainAccessToken();
        }

        while (tries < _maxRetries)
        {
            tries++;

            // Clone the original request so we have a copy in case of a failure
            HttpRequestMessage clonedRequest = await CloneHttpRequestMessageAsync(request);

            clonedRequest.Headers.Add("Authorization", "Bearer " + _accessToken);

            // Send request
            try
            {
                response = await base.SendAsync(clonedRequest, cancellationToken);
            }
            catch (HttpRequestException)
            {
                // HDC request error, wait a bit and try again
                Thread.Sleep(2000);
                continue;
            }
            catch (SocketException)
            {
                // HDC timed out, wait a bit and try again
                Thread.Sleep(2000);
                continue;
            }
            catch (TaskCanceledException tcex)
            {
                if (!tcex.CancellationToken.IsCancellationRequested)
                {
                    // HDC time out, wait a bit and try again
                    Thread.Sleep(2000);
                    continue;
                }
                else
                {
                    throw tcex;
                }
            }

            // If unauthorized, the token likely expired so get a new one and retry
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await ObtainAccessToken();
                continue;
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                // Sometimes HDC returns 500 errors so wait a bit then retry once instead of failing the call.
                Thread.Sleep(2000);
                continue;
            }

            break;
        }

        if (response == null)
        {
            throw new HttpRetriesExhaustedException("AuthorizationHandler: NULL response, unable to communicate with Hardware Dev Center");
        }

        return response;
    }

    private async Task<AccessToken> ObtainAccessToken(CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(_authCredentials.ManagedIdentityClientId))
        {
            return await GetClientAssertionTokenAsync(cancellationToken);
        }
        else
        {
            return await GetTokenUsingClientSecretAsync(cancellationToken);
        }
    }

    private async Task<AccessToken> GetClientAssertionTokenAsync(CancellationToken cancellationToken)
    {
        AccessToken token = default;

        var clientAssertionCredential = new ClientAssertionCredential(
            _authCredentials.TenantId,
            _authCredentials.ClientId,
            async (token) => await GetTokenUsingManagedIdentityAsync(cancellationToken)
        );

        token = await clientAssertionCredential.GetTokenAsync(
            new TokenRequestContext(new[] { "https://manage.devcenter.microsoft.com/.default" }),
            cancellationToken
        );

        if (!string.IsNullOrEmpty(token.Token))
        {
            _accessToken = token.Token;
        }

        return token;
    }

    /// <summary>
    /// Callback function for <see cref="GetClientAssertionTokenAsync"/>
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<string> GetTokenUsingManagedIdentityAsync(CancellationToken cancellationToken)
    {
        var credential = new ManagedIdentityCredential(_authCredentials.ManagedIdentityClientId);

        var token = await credential.GetTokenAsync(
            new TokenRequestContext(new[] { _authCredentials.Scope }),
            cancellationToken
        );

        return token.Token;
    }

    private async Task<AccessToken> GetTokenUsingClientSecretAsync(CancellationToken cancellationToken)
    {

        var credential = new ClientSecretCredential(
            _authCredentials.TenantId,
            _authCredentials.ClientId,
            _authCredentials.Key
        );

        var token = await credential.GetTokenAsync(
            new TokenRequestContext(new[] { "https://manage.devcenter.microsoft.com/.default" }),
            cancellationToken
        );

        if (!string.IsNullOrEmpty(token.Token))
        {
            _accessToken = token.Token;
        }

        return token;
    }

    //
    // https://stackoverflow.com/questions/21467018/how-to-forward-an-httprequestmessage-to-another-server
    //
    public static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage request)
    {
        HttpRequestMessage clone = new(request.Method, request.RequestUri);

        // Copy the request's content (via a MemoryStream) into the cloned object
        MemoryStream ms = new();
        if (request.Content != null)
        {
            await request.Content.CopyToAsync(ms).ConfigureAwait(false);
            ms.Position = 0;
            clone.Content = new StreamContent(ms);

            // Copy the content headers
            if (request.Content.Headers != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> h in request.Content.Headers)
                {
                    clone.Content.Headers.Add(h.Key, h.Value);
                }
            }
        }
        clone.Version = request.Version;

        foreach (KeyValuePair<string, object> prop in request.Properties)
        {
            clone.Properties.Add(prop);
        }

        foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return clone;
    }
}

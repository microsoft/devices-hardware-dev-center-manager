/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public interface IDevCenterHandler
    {
        Task<DevCenterResponse<bool>> CancelShippingLabel(string productId, string submissionId, string shippingLabelId);
        Task<DevCenterResponse<bool>> CommitSubmission(string productId, string submissionId);
        Task<DevCenterResponse<bool>> CreateMetaData(string productId, string submissionId);
        Task<DevCenterResponse<Audience>> GetAudiences();
        Task<DevCenterResponse<Submission>> GetPartnerSubmission(string publisherId, string productId, string submissionId);
        Task<DevCenterResponse<Product>> GetProducts(string productId = null);
        Task<DevCenterResponse<ShippingLabel>> GetShippingLabels(string productId, string submissionId, string shippingLabelId = null);
        Task<DevCenterResponse<Submission>> GetSubmission(string productId, string submissionId = null);
        Task<DevCenterResponse<Output>> HdcGet<Output>(string uri, bool isMany) where Output : IArtifact;
        Task<DevCenterResponse<Output>> HdcPost<Output>(string uri, object input) where Output : IArtifact;
        Task<DevCenterErrorDetails> InvokeHdcService(HttpMethod method, string uri, object input, Action<string> processContent);
        Task<DevCenterResponse<Output>> InvokeHdcService<Output>(HttpMethod method, string uri, object input, bool isMany) where Output : IArtifact;
        Task<DevCenterResponse<Product>> NewProduct(NewProduct input);
        Task<DevCenterResponse<ShippingLabel>> NewShippingLabel(string productId, string submissionId, NewShippingLabel shippingLabelInfo);
        Task<DevCenterResponse<Submission>> NewSubmission(string productId, NewSubmission submissionInfo);
    }
}
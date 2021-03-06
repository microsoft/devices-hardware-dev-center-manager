﻿/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class DevCenterErrorDetails
    {
        [JsonProperty("headers")]
        public HttpResponseHeaders Headers { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("validationErrors")]
        public IList<DevCenterErrorValidationErrorEntry> ValidationErrors { get; set; }

        [JsonProperty("httpErrorCode")]
        public int? HttpErrorCode { get; set; }

        [JsonProperty("trace")]
        public DevCenterTrace Trace { get; set; }
    }

    public class DevCenterErrorValidationErrorEntry
    {
        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

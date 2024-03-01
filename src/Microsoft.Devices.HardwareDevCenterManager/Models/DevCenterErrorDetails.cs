/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.  
--*/

using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class DevCenterErrorDetails
{
    [JsonPropertyName("headers")]
    public HttpResponseHeaders Headers { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("validationErrors")]
    public IList<DevCenterErrorValidationErrorEntry> ValidationErrors { get; set; }

    [JsonPropertyName("httpErrorCode")]
    public int? HttpErrorCode { get; set; }

    [JsonPropertyName("trace")]
    public DevCenterTrace Trace { get; set; }
}

public class DevCenterErrorValidationErrorEntry
{
    [JsonPropertyName("target")]
    public string Target { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}

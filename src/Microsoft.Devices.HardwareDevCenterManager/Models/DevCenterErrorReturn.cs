/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class DevCenterErrorReturn
{
    [JsonPropertyName("error")]
    public DevCenterErrorDetails Error { get; set; }

    [JsonPropertyName("statusCode")]
    public string StatusCode { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("httpErrorCode")]
    public int? HttpErrorCode { get; set; }

    [JsonPropertyName("validationErrors")]
    public IList<DevCenterErrorValidationErrorEntry> ValidationErrors;
}

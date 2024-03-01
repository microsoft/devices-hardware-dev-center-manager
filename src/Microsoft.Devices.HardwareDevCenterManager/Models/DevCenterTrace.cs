/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class DevCenterTrace
{
    [JsonPropertyName("correlationId")]
    public string CorrelationId { get; set; }

    [JsonPropertyName("requestId")]
    public string RequestId { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("method")]
    public string Method { get; set; }
}

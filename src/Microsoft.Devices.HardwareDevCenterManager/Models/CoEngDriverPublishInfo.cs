/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using Microsoft.Devices.HardwareDevCenterManager.Utility;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class CoEngDriverPublishInfo
{
    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("flooringBuildNumber")]
    public string FlooringBuildNumber { get; set; }

    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("ceilingBuildNumber")]
    public string CeilingBuildNumber { get; set; }
}

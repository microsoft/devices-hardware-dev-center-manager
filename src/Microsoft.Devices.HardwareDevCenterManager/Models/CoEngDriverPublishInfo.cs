/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.  
--*/

using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class CoEngDriverPublishInfo
{
    [JsonPropertyName("flooringBuildNumber")]
    public string FlooringBuildNumber { get; set; }

    [JsonPropertyName("ceilingBuildNumber")]
    public string CeilingBuildNumber { get; set; }
}

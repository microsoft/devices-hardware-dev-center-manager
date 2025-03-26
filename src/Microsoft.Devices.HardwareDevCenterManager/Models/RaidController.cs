/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class RaidController
{
    [JsonPropertyName("usedProprietary")]
    public bool UsedProprietary { get; set; }

    [JsonPropertyName("usedMicrosoft")]
    public bool UsedMicrosoft { get; set; }

    [JsonPropertyName("isThirdPartyNeeded")]
    public bool IsThirdPartyNeeded { get; set; }

    [JsonPropertyName("isSES")]
    public bool IsSES { get; set; }

    [JsonPropertyName("isSAFTE")]
    public bool IsSAFTE { get; set; }
}

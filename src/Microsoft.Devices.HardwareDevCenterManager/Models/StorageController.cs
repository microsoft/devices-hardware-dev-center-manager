/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.  
--*/

using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class StorageController
{
    [JsonPropertyName("usedProprietary")]
    public bool UsedProprietary { get; set; }

    [JsonPropertyName("usedMicrosoft")]
    public bool UsedMicrosoft { get; set; }

    [JsonPropertyName("usedBootSupport")]
    public bool UsedBootSupport { get; set; }

    [JsonPropertyName("usedBetterBoot")]
    public bool UsedBetterBoot { get; set; }

    [JsonPropertyName("supportsSector4K512E")]
    public bool SupportsSector4K512E { get; set; }

    [JsonPropertyName("supportsSector4K4K")]
    public bool SupportsSector4K4K { get; set; }

    [JsonPropertyName("supportsDifferential")]
    public bool SupportsDifferential { get; set; }
}

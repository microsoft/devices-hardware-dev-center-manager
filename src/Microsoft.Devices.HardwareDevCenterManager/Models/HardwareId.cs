/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class HardwareId
{
    [JsonPropertyName("bundleId")]
    public string BundleId { get; set; }

    [JsonPropertyName("infId")]
    public string InfId { get; set; }

    [JsonPropertyName("operatingSystemCode")]
    public string OperatingSystemCode { get; set; }

    [JsonPropertyName("pnpString")]
    public string PnpString { get; set; }

    [JsonPropertyName("distributionState")]
    public string DistributionState { get; set; }
}

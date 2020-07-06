/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class HardwareId
    {
        [JsonProperty("bundleId")]
        public string BundleId { get; set; }

        [JsonProperty("infId")]
        public string InfId { get; set; }

        [JsonProperty("operatingSystemCode")]
        public string OperatingSystemCode { get; set; }

        [JsonProperty("pnpString")]
        public string PnpString { get; set; }

        [JsonProperty("distributionState")]
        public string DistributionState { get; set; }
    }
}

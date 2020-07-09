/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class StorageController
    {
        [JsonProperty("usedProprietary")]
        public bool UsedProprietary { get; set; }

        [JsonProperty("usedMicrosoft")]
        public bool UsedMicrosoft { get; set; }

        [JsonProperty("usedBootSupport")]
        public bool UsedBootSupport { get; set; }

        [JsonProperty("usedBetterBoot")]
        public bool UsedBetterBoot { get; set; }

        [JsonProperty("supportsSector4K512E")]
        public bool SupportsSector4K512E { get; set; }

        [JsonProperty("supportsSector4K4K")]
        public bool SupportsSector4K4K { get; set; }

        [JsonProperty("supportsDifferential")]
        public bool SupportsDifferential { get; set; }
    }
}

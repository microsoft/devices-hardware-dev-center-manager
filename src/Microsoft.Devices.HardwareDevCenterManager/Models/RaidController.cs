/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class RaidController
    {
        [JsonProperty("usedProprietary")]
        public bool UsedProprietary { get; set; }

        [JsonProperty("usedMicrosoft")]
        public bool UsedMicrosoft { get; set; }

        [JsonProperty("isThirdPartyNeeded")]
        public bool IsThirdPartyNeeded { get; set; }

        [JsonProperty("isSES")]
        public bool IsSES { get; set; }

        [JsonProperty("isSAFTE")]
        public bool IsSAFTE { get; set; }
    }
}

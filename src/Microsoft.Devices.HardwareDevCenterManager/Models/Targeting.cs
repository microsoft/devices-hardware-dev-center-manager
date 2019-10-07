/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class Targeting
    {
        [JsonProperty("hardwareIds")]
        public List<HardwareId> HardwareIds { get; set; }

        [JsonProperty("chids")]
        public List<CHID> Chids { get; set; }

        [JsonProperty("restrictedToAudiences")]
        public List<string> RestrictedToAudiences { get; set; }

        [JsonProperty("inServicePublishInfo")]
        public InServicePublishInfo InServicePublishInfo { get; set; }
    }
}

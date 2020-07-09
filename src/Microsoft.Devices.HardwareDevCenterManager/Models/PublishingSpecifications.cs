/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class PublishingSpecifications
    {
        [JsonProperty("goLiveDate")]
        public DateTime GoLiveDate { get; set; }

        [JsonProperty("visibleToAccounts")]
        public List<string> VisibleToAccounts { get; set; }

        [JsonProperty("isAutoInstallDuringOSUpgrade")]
        public bool IsAutoInstallDuringOSUpgrade { get; set; }

        [JsonProperty("isAutoInstallOnApplicableSystems")]
        public bool IsAutoInstallOnApplicableSystems { get; set; }

        [JsonProperty("manualAcquisition")]
        public bool ManualAcquisition { get; set; }

        [JsonProperty("isDisclosureRestricted")]
        public bool IsDisclosureRestricted { get; set; }

        [JsonProperty("publishToWindows10s")]
        public bool PublishToWindows10S { get; set; }

        [JsonProperty("additionalInfoForMsApproval")]
        public AdditionalInfoForMsApproval AdditionalInfoForMsApproval { get; set; }
    }
}

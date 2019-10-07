/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class AdditionalInfoForMsApproval
    {
        [JsonProperty("microsoftContact")]
        public string MicrosoftContact { get; set; }

        [JsonProperty("validationsPerformed")]
        public string ValidationsPerformed { get; set; }

        [JsonProperty("affectedOems")]
        public List<string> AffectedOems { get; set; }

        [JsonProperty("isRebootRequired")]
        public bool IsRebootRequired { get; set; }

        [JsonProperty("isCoEngineered")]
        public bool IsCoEngineered { get; set; }

        [JsonProperty("isForUnreleasedHardware")]
        public bool IsForUnreleasedHardware { get; set; }

        [JsonProperty("hasUiSoftware")]
        public bool HasUiSoftware { get; set; }

        [JsonProperty("businessJustification")]
        public string BusinessJustification { get; set; }
    }
}

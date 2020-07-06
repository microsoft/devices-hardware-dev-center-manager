/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class CHID
    {
        [JsonProperty("chid")]
        public string Chid { get; set; }

        [JsonProperty("distributionState")]
        public string DistributionState { get; set; }

    }
}

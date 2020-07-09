/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class CoEngDriverPublishInfo
    {
        [JsonProperty("flooringBuildNumber")]
        public string FlooringBuildNumber { get; set; }

        [JsonProperty("ceilingBuildNumber")]
        public string CeilingBuildNumber { get; set; }
    }
}

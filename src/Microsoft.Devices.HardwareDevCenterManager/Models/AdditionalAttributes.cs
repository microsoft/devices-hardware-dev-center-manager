/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class AdditionalAttributes
    {
        [JsonProperty("storageController")]
        public StorageController StorageController { get; set; }

        [JsonProperty("raidController")]
        public RaidController RaidController { get; set; }

        [JsonProperty("svvp")]
        public Svvp Svvp { get; set; }
    }
}

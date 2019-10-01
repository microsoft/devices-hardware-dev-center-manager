/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class RecipientSpecifications
    {
        [JsonProperty("receiverPublisherId")]
        public string ReceiverPublisherId { get; set; }
        [JsonProperty("enforceChidTargeting")]
        public bool EnforceChidTargeting { get; set; }
    }
}

/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class NewShippingLabel
    {
        [JsonProperty("publishingSpecifications")]
        public PublishingSpecifications PublishingSpecifications { get; set; }

        [JsonProperty("targeting")]
        public Targeting Targeting { get; set; }

        [JsonProperty("recipientSpecifications")]
        public RecipientSpecifications RecipientSpecifications { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }
    }
}

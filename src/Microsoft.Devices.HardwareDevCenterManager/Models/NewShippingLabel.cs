/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.  
--*/

using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class NewShippingLabel
{
    [JsonPropertyName("publishingSpecifications")]
    public PublishingSpecifications PublishingSpecifications { get; set; }

    [JsonPropertyName("targeting")]
    public Targeting Targeting { get; set; }

    [JsonPropertyName("recipientSpecifications")]
    public RecipientSpecifications RecipientSpecifications { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("destination")]
    public string Destination { get; set; }
}

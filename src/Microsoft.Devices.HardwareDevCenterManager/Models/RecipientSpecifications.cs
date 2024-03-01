/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.  
--*/

using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class RecipientSpecifications
{
    [JsonPropertyName("receiverPublisherId")]
    public string ReceiverPublisherId { get; set; }

    [JsonPropertyName("enforceChidTargeting")]
    public bool EnforceChidTargeting { get; set; }
}

/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class Targeting
{
    [JsonPropertyName("hardwareIds")]
    public List<HardwareId> HardwareIds { get; set; }

    [JsonPropertyName("chids")]
    public List<CHID> Chids { get; set; }

    [JsonPropertyName("restrictedToAudiences")]
    public List<string> RestrictedToAudiences { get; set; }

    [JsonPropertyName("inServicePublishInfo")]
    public InServicePublishInfo InServicePublishInfo { get; set; }

    [JsonPropertyName("coEngDriverPublishInfo")]
    public CoEngDriverPublishInfo CoEngDriverPublishInfo { get; set; }
}

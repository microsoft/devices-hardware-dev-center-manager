/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class InServicePublishInfo
{
    [JsonPropertyName("flooring")]
    public string Flooring { get; set; }

    [JsonPropertyName("ceiling")]
    public string Ceiling { get; set; }
}

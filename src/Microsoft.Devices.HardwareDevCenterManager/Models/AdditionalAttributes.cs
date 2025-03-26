/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class AdditionalAttributes
{
    [JsonPropertyName("storageController")]
    public StorageController StorageController { get; set; }

    [JsonPropertyName("raidController")]
    public RaidController RaidController { get; set; }

    [JsonPropertyName("svvp")]
    public Svvp Svvp { get; set; }
}

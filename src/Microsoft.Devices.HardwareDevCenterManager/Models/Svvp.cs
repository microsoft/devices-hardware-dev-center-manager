/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using Microsoft.Devices.HardwareDevCenterManager.Utility;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class Svvp
{
    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("maxProcessors")]
    public string MaxProcessors { get; set; }

    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("maxMemory")]
    public string MaxMemory { get; set; }
}

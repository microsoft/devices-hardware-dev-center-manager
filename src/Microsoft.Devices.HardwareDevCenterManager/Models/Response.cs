/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.  
--*/

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class Response<T>
{
    [JsonPropertyName("value")]
    public List<T> Value { get; set; }

    [JsonPropertyName("links")]
    public List<Link> Links { get; set; }
}

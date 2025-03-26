/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class Link
{
    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonPropertyName("rel")]
    public string Rel { get; set; }

    [JsonPropertyName("method")]
    public string Method { get; set; }

    public void Dump()
    {
        Console.WriteLine("               - href:   " + Href);
        Console.WriteLine("               - method: " + Method);
        Console.WriteLine("               - rel:    " + Rel);
    }
}

/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class Audience : IArtifact
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("audienceName")]
    public string AudienceName { get; set; }

    [JsonPropertyName("links")]
    public List<Link> Links { get; set; }

    public void Dump()
    {
        Console.WriteLine("---- Audience: " + Id);
        Console.WriteLine("         audienceName: " + AudienceName);
        Console.WriteLine("         description:  " + Description);
        Console.WriteLine("         name:         " + Name);
        Console.WriteLine("         Links:");
        if (Links != null)
        {
            foreach (Link link in Links)
            {
                link.Dump();
            }
        }
    }
}

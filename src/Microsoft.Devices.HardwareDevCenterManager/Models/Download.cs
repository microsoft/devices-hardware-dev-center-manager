﻿/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class Download
{
    [JsonPropertyName("items")]
    public List<Item> Items { get; set; }

    [JsonPropertyName("messages")]
    public List<string> Messages { get; set; }

    public class Item
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }
    }

    public enum Type
    {
        initialPackage,
        signedPackage,
        certificationReport,
        driverMetadata,
        derivedPackage
    }

    public void Dump()
    {
        foreach (Item item in Items)
        {
            Console.WriteLine("               - url:  " + item.Url);
            Console.WriteLine("               - type: " + item.Type);
        }
        Console.WriteLine("               - messages:");

        if (Messages != null)
        {
            foreach (string msg in Messages)
            {
                Console.WriteLine("                   " + msg);
            }
        }
    }
}

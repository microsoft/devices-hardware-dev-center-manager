/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class InServicePublishInfo
    {
        [JsonProperty("flooring")]
        public string Flooring { get; set; }

        [JsonProperty("ceiling")]
        public string Ceiling { get; set; }
    }
}

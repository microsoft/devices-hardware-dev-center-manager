/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class Svvp
    {
        [JsonProperty("maxProcessors")]
        public string MaxProcessors { get; set; }

        [JsonProperty("maxMemory")]
        public string MaxMemory { get; set; }
    }
}

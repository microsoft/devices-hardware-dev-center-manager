/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.  
--*/

using Microsoft.Devices.HardwareDevCenterManager.Utility;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class WorkflowStatus
{
    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("currentStep")]
    public string CurrentStep { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("messages")]
    public List<string> Messages { get; set; }

    [JsonPropertyName("errorReport")]
    public string ErrorReport { get; set; }

    public async Task Dump()
    {
        Console.WriteLine("> Step:  {0}", CurrentStep);
        Console.WriteLine("> State: {0}", State);
        if (Messages != null)
        {
            foreach (string msg in Messages)
            {
                Console.WriteLine("> Message: {0}", msg);
            }
        }
        if (ErrorReport != null)
        {
            Console.WriteLine("> Error Report:");
            Utility.BlobStorageHandler bsh = new(ErrorReport);
            string errorContent = await bsh.DownloadToString();
            Console.WriteLine(errorContent);
            Console.WriteLine();
        }

        return;
    }
}

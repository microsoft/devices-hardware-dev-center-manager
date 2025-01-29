/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using Microsoft.Devices.HardwareDevCenterManager.Utility;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class Submission : IArtifact
{
    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("productId")]
    public string ProductId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
    public enum SubmissionType
    {
        [JsonPropertyName("initial")]
        Initial,
        [JsonPropertyName("derived")]
        Derived
    }

    [JsonPropertyName("commitStatus")]
    public string CommitStatus { get; set; }

    [JsonPropertyName("isExtensionInf")]
    public bool IsExtensionInf { get; set; }

    [JsonPropertyName("isUniversal")]
    public bool IsUniversal { get; set; }

    [JsonPropertyName("isDeclarativeInf")]
    public bool IsDeclarativeInf { get; set; }

    [JsonPropertyName("createdBy")]
    public string CreatedBy { get; set; }

    [JsonPropertyName("createdDateTime")]
    public string CreatedDateTime { get; set; }

    [JsonPropertyName("links")]
    public List<Link> Links { get; set; }

    [JsonPropertyName("workflowStatus")]
    public WorkflowStatus WorkflowStatus { get; set; }

    [JsonPropertyName("downloads")]
    public Download Downloads { get; set; }

    public async void Dump()
    {
        Console.WriteLine("---- Submission: " + Id);
        Console.WriteLine("         Name:           " + Name);
        Console.WriteLine("         ProductId:      " + ProductId);
        Console.WriteLine("         type:           " + Type ?? "");
        Console.WriteLine("         commitStatus:   " + CommitStatus ?? "");
        Console.WriteLine("         isExtensionInf: " + IsExtensionInf ?? "");
        Console.WriteLine("         isUniversal:    " + IsUniversal ?? "");
        Console.WriteLine("         isDeclarativeInf: " + IsDeclarativeInf ?? "");
        Console.WriteLine("         CreatedBy:      " + CreatedBy ?? "");
        Console.WriteLine("         CreateTime:     " + CreatedDateTime ?? "");
        Console.WriteLine("         Links:");
        if (Links != null)
        {
            foreach (Link link in Links)
            {
                link.Dump();
            }
        }
        Console.WriteLine("         Status:");
        if (WorkflowStatus != null)
        {
            await WorkflowStatus.Dump();
        }
        Console.WriteLine("         Downloads:");
        Console.WriteLine("               - messages:");
        Downloads?.Dump();
        Console.WriteLine();
    }
}

public class NewSubmission
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

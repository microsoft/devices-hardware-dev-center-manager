/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class Submission : IArtifact
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        public enum SubmissionType
        {
            [JsonProperty("initial")]
            Initial,
            [JsonProperty("derived")]
            Derived
        }

        [JsonProperty("commitStatus")]
        public string CommitStatus { get; set; }
        
        [JsonProperty("isExtensionInf")]
        public bool IsExtensionInf { get; set; }

        [JsonProperty("isUniversal")]
        public bool isUniversal { get; set; }

        [JsonProperty("isDeclarativeInf")]
        public bool isDeclarativeInf { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("createdDateTime")]
        public string CreatedDateTime { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }

        [JsonProperty("workflowStatus")]
        public WorkflowStatus WorkflowStatus { get; set; }

        [JsonProperty("downloads")]
        public Download Downloads { get; set; }

        public async void Dump()
        {
            Console.WriteLine("---- Submission: " + Id);
            Console.WriteLine("         Name:           " + Name);
            Console.WriteLine("         ProductId:      " + ProductId);
            Console.WriteLine("         type:           " + Type ?? "");
            Console.WriteLine("         commitStatus:   " + CommitStatus ?? "");
            Console.WriteLine("         isExtensionInf: " + IsExtensionInf ?? "");
            Console.WriteLine("         isUniversal:    " + isUniversal ?? "");
            Console.WriteLine("         isDeclarativeInf: " + isDeclarativeInf ?? "");
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
            if (Downloads != null)
            {
                Downloads.Dump();
            }
            Console.WriteLine();
        }
    }

    public class NewSubmission
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}

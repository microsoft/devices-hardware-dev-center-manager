/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class ShippingLabel : IArtifact
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("submissionId")]
        public string SubmissionId { get; set; }

        [JsonProperty("publishingSpecifications")]
        public PublishingSpecifications PublishingSpecifications { get; set; }

        [JsonProperty("workflowStatus")]
        public WorkflowStatus WorkflowStatus { get; set; }

        [JsonProperty("links")]
        public List<Link> Links;

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        public async void Dump()
        {
            Console.WriteLine("---- Shipping Label: " + Id);
            Console.WriteLine("         Name:        " + Name);
            Console.WriteLine("         ProductId:   " + ProductId);
            Console.WriteLine("         SubmissionId:" + SubmissionId);

            Console.WriteLine("         Publishing Specifications:");
            if (PublishingSpecifications != null)
            {
                Console.WriteLine("           publishToWindows10s:" + PublishingSpecifications.PublishToWindows10S);
                Console.WriteLine("           isDisclosureRestricted:" + PublishingSpecifications.IsDisclosureRestricted);
                Console.WriteLine("           isAutoInstallOnApplicableSystems:" + PublishingSpecifications.IsAutoInstallOnApplicableSystems);
                Console.WriteLine("           isAutoInstallDuringOSUpgrade:" + PublishingSpecifications.IsAutoInstallDuringOSUpgrade);
                Console.WriteLine("           goLiveDate:" + PublishingSpecifications.GoLiveDate);
                Console.WriteLine("           additionalInfoForMsApproval:");
                Console.WriteLine("               businessJustification:" + PublishingSpecifications.AdditionalInfoForMsApproval.BusinessJustification);
                Console.WriteLine("               hasUiSoftware:" + PublishingSpecifications.AdditionalInfoForMsApproval.HasUiSoftware);
                Console.WriteLine("               isCoEngineered:" + PublishingSpecifications.AdditionalInfoForMsApproval.IsCoEngineered);
                Console.WriteLine("               isForUnreleasedHardware:" + PublishingSpecifications.AdditionalInfoForMsApproval.IsForUnreleasedHardware);
                Console.WriteLine("               isRebootRequired:" + PublishingSpecifications.AdditionalInfoForMsApproval.IsRebootRequired);
                Console.WriteLine("               microsoftContact:" + PublishingSpecifications.AdditionalInfoForMsApproval.MicrosoftContact);
                Console.WriteLine("               validationsPerformed:" + PublishingSpecifications.AdditionalInfoForMsApproval.ValidationsPerformed);
                Console.WriteLine("               affectedOems:");
                foreach (string oem in PublishingSpecifications.AdditionalInfoForMsApproval.AffectedOems)
                {
                    Console.WriteLine("                            " + oem);
                }
            }

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
            Console.WriteLine();
        }
    }
}

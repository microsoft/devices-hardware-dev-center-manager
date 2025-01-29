/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using Microsoft.Devices.HardwareDevCenterManager.Utility;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class ShippingLabel : IArtifact
{
    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("productId")]
    public string ProductId { get; set; }

    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("submissionId")]
    public string SubmissionId { get; set; }

    [JsonPropertyName("publishingSpecifications")]
    public PublishingSpecifications PublishingSpecifications { get; set; }

    [JsonPropertyName("targeting")]
    public Targeting Targeting { get; set; }

    [JsonPropertyName("workflowStatus")]
    public WorkflowStatus WorkflowStatus { get; set; }

    [JsonPropertyName("links")]
    public List<Link> Links;

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("destination")]
    public string Destination { get; set; }

    public async void Dump()
    {
        Console.WriteLine("---- Shipping Label:  " + Id);
        Console.WriteLine("         Name:        " + Name);
        Console.WriteLine("         ProductId:   " + ProductId);
        Console.WriteLine("         SubmissionId: " + SubmissionId);
        Console.WriteLine("         Destination: " + Destination);

        Console.WriteLine("         Publishing Specifications:");
        if (PublishingSpecifications != null)
        {
            Console.WriteLine("           publishToWindows10s:    " + PublishingSpecifications.PublishToWindows10S);
            Console.WriteLine("           isDisclosureRestricted: " + PublishingSpecifications.IsDisclosureRestricted);
            Console.WriteLine("           isAutoInstallOnApplicableSystems: " + PublishingSpecifications.IsAutoInstallOnApplicableSystems);
            Console.WriteLine("           isAutoInstallDuringOSUpgrade:     " + PublishingSpecifications.IsAutoInstallDuringOSUpgrade);
            Console.WriteLine("           goLiveDate:             " + PublishingSpecifications.GoLiveDate);
            Console.WriteLine("           additionalInfoForMsApproval:");
            Console.WriteLine("               businessJustification: " + PublishingSpecifications.AdditionalInfoForMsApproval.BusinessJustification);
            Console.WriteLine("               hasUiSoftware:         " + PublishingSpecifications.AdditionalInfoForMsApproval.HasUiSoftware);
            Console.WriteLine("               isCoEngineered:        " + PublishingSpecifications.AdditionalInfoForMsApproval.IsCoEngineered);
            Console.WriteLine("               isForUnreleasedHardware: " + PublishingSpecifications.AdditionalInfoForMsApproval.IsForUnreleasedHardware);
            Console.WriteLine("               isRebootRequired:      " + PublishingSpecifications.AdditionalInfoForMsApproval.IsRebootRequired);
            Console.WriteLine("               microsoftContact:      " + PublishingSpecifications.AdditionalInfoForMsApproval.MicrosoftContact);
            Console.WriteLine("               validationsPerformed:  " + PublishingSpecifications.AdditionalInfoForMsApproval.ValidationsPerformed);
            Console.WriteLine("               affectedOems:");
            foreach (string oem in PublishingSpecifications.AdditionalInfoForMsApproval.AffectedOems)
            {
                Console.WriteLine("                            " + oem);
            }
        }

        Console.WriteLine("         Targeting:");
        if (Targeting != null)
        {
            // hardware ids
            Console.WriteLine("           hardwareIds:");
            if (Targeting.HardwareIds.Count > 0)
            {
                foreach (HardwareId hid in Targeting.HardwareIds)
                {
                    Console.WriteLine("           bundledId: " + hid.BundleId);
                    Console.WriteLine("           infId:     " + hid.InfId);
                    Console.WriteLine("           operatingSystemCode: " + hid.OperatingSystemCode);
                    Console.WriteLine("           pnpString: " + hid.PnpString);
                    Console.WriteLine("           distributionsState:  " + hid.DistributionState);
                }
            }

            // chids
            Console.WriteLine("           chids:");
            if (Targeting.Chids.Count > 0)
            {
                foreach (CHID chid in Targeting.Chids)
                {
                    Console.WriteLine("           chid: " + chid.Chid);
                    Console.WriteLine("           distributionState: " + chid.DistributionState);
                }
            }

            // audiences
            Console.WriteLine("           restrictedToAudiences:");
            if (Targeting.RestrictedToAudiences.Count > 0)
            {
                foreach (string audience in Targeting.RestrictedToAudiences)
                {
                    Console.WriteLine("           " + audience);
                }
            }

            //  in service publish information
            Console.WriteLine("           inServicePublishInfo:");
            if (Targeting.InServicePublishInfo != null)
            {
                Console.WriteLine("               flooring: " + Targeting.InServicePublishInfo.Flooring);
                Console.WriteLine("               ceiling:  " + Targeting.InServicePublishInfo.Ceiling);
            }

            //  co-engineering driver publish information
            Console.WriteLine("           coEngDriverPublishInfo:");
            if (Targeting.CoEngDriverPublishInfo != null)
            {
                Console.WriteLine("               flooringBuildNumber: " + Targeting.CoEngDriverPublishInfo.FlooringBuildNumber);
                Console.WriteLine("               ceilingBuildNumber:  " + Targeting.CoEngDriverPublishInfo.CeilingBuildNumber);
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

/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.  
--*/

using Microsoft.Devices.HardwareDevCenterManager.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class Product : IArtifact
{
    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonConverter(typeof(LongToStringJsonConverter))]
    [JsonPropertyName("sharedProductId")]
    public string SharedProductId { get; set; }

    [JsonPropertyName("productName")]
    public string ProductName { get; set; }

    [JsonPropertyName("productType")]
    public string ProductType { get; set; }

    [JsonPropertyName("firmwareVersion")]
    public string FirmwareVersion { get; set; }

    [JsonPropertyName("deviceType")]
    public string DeviceType { get; set; }

    [JsonPropertyName("isTestSign")]
    public bool IsTestSign { get; set; }

    [JsonPropertyName("isFlightSign")]
    public bool IsFlightSign { get; set; }

    [JsonPropertyName("requestedSignatures")]
    public List<string> RequestedSignatures { get; set; }

    [JsonPropertyName("createdBy")]
    public string CreatedBy { get; set; }

    [JsonPropertyName("updatedBy")]
    public string UpdatedBy { get; set; }

    [JsonPropertyName("createdDateTime")]
    public DateTime CreatedDateTime { get; set; }

    [JsonPropertyName("updatedDateTime")]
    public DateTime UpdatedDateTime { get; set; }

    [JsonPropertyName("announcementDate")]
    public DateTime AnnouncementDate { get; set; }

    [JsonPropertyName("deviceMetadataIds")]
    public List<string> DeviceMetadataIds { get; set; }

    [JsonPropertyName("marketingNames")]
    public List<string> MarketingNames { get; set; }

    [JsonPropertyName("testHarness")]
    public string TestHarness { get; set; }

    [JsonPropertyName("selectedProductTypes")]
    public Dictionary<string, string> SelectedProductTypes { get; set; }

    [JsonPropertyName("isCommitted")]
    public bool IsCommitted { get; set; }

    [JsonPropertyName("isRetpolineCompiled")]
    public bool IsRetpolineCompiled { get; set; }

    [JsonPropertyName("additionalAttributes")]
    public AdditionalAttributes AdditionalAttributes { get; set; }

    public void Dump()
    {
        Console.WriteLine("---- Product: " + Id);
        Console.WriteLine("         Name:         " + ProductName ?? "");
        Console.WriteLine("         Shared Id:    " + SharedProductId ?? "");
        Console.WriteLine("         Type:         " + ProductType ?? "");
        Console.WriteLine("         DevType:      " + DeviceType ?? "");
        Console.WriteLine("         FWVer:        " + FirmwareVersion ?? "");
        Console.WriteLine("         isTestSign:   " + IsTestSign ?? "");
        Console.WriteLine("         isFlightSign: " + IsFlightSign ?? "");
        Console.WriteLine("         isCommitted:  " + IsCommitted ?? "");
        Console.WriteLine("         isRetpolineCompiled: " + IsRetpolineCompiled ?? "");

        Console.WriteLine("         createdBy: " + CreatedBy ?? "");
        Console.WriteLine("         updatedBy: " + UpdatedBy ?? "");
        Console.WriteLine("         createdDateTime:  " + CreatedDateTime.ToString("s", CultureInfo.CurrentCulture));
        Console.WriteLine("         updatedDateTime:  " + UpdatedDateTime.ToString("s", CultureInfo.CurrentCulture));
        Console.WriteLine("         announcementDate: " + AnnouncementDate.ToString("s", CultureInfo.CurrentCulture));
        Console.WriteLine("         testHarness: " + TestHarness ?? "");

        Console.WriteLine("         Signatures:");
        foreach (string sig in RequestedSignatures)
        {
            Console.WriteLine("                   " + sig);
        }

        Console.WriteLine("         deviceMetadataIds:");
        if (DeviceMetadataIds != null)
        {

            foreach (string sig in DeviceMetadataIds)
            {
                Console.WriteLine("                   " + sig);
            }
        }
        Console.WriteLine("         selectedProductTypes:");
        if (SelectedProductTypes != null)
        {
            foreach (KeyValuePair<string, string> entry in SelectedProductTypes)
            {
                Console.WriteLine("                   " + entry.Key + ": " + entry.Value);
            }
        }

        Console.WriteLine("         marketingNames:");
        if (MarketingNames != null)
        {
            foreach (string sig in MarketingNames)
            {
                Console.WriteLine("                   " + sig);
            }
        }

        Console.WriteLine("         additionalAttributes:");
        if (AdditionalAttributes != null)
        {
            if (AdditionalAttributes.StorageController != null)
            {
                Console.WriteLine("         storageController:");
                Console.WriteLine("             usedProprietary:    " + AdditionalAttributes.StorageController.UsedProprietary);
                Console.WriteLine("             usedMicrosoft:      " + AdditionalAttributes.StorageController.UsedMicrosoft);
                Console.WriteLine("             usedBootSupport:    " + AdditionalAttributes.StorageController.UsedBootSupport);
                Console.WriteLine("             usedBetterBoot:     " + AdditionalAttributes.StorageController.UsedBetterBoot);
                Console.WriteLine("             supportsSector4K512E: " + AdditionalAttributes.StorageController.SupportsSector4K512E);
                Console.WriteLine("             supportsSector4K4K:   " + AdditionalAttributes.StorageController.SupportsSector4K4K);
                Console.WriteLine("             supportsDifferential: " + AdditionalAttributes.StorageController.SupportsDifferential);
            }

            if (AdditionalAttributes.RaidController != null)
            {
                Console.WriteLine("         raidController:");
                Console.WriteLine("             usedProprietary: " + AdditionalAttributes.RaidController.UsedProprietary);
                Console.WriteLine("             usedMicrosoft:   " + AdditionalAttributes.RaidController.UsedMicrosoft);
                Console.WriteLine("             isThirdPartyNeeded: " + AdditionalAttributes.RaidController.IsThirdPartyNeeded);
                Console.WriteLine("             isSES:   " + AdditionalAttributes.RaidController.IsSES);
                Console.WriteLine("             isSAFTE: " + AdditionalAttributes.RaidController.IsSAFTE);
            }

            if (AdditionalAttributes.Svvp != null)
            {
                Console.WriteLine("         svvp:");
                Console.WriteLine("             maxProcessors: " + AdditionalAttributes.Svvp.MaxProcessors);
                Console.WriteLine("             maxMemory:     " + AdditionalAttributes.Svvp.MaxMemory);
            }
        }
        Console.WriteLine();
    }
}

public class NewProduct
{
    [JsonPropertyName("productName")]
    public string ProductName { get; set; }

    [JsonPropertyName("testHarness")]
    public string TestHarness { get; set; }

    [JsonPropertyName("announcementDate")]
    public DateTime AnnouncementDate { get; set; }

    [JsonPropertyName("deviceMetadataIds")]
    public List<string> DeviceMetadataIds { get; set; }

    [JsonPropertyName("firmwareVersion")]
    public string FirmwareVersion { get; set; }

    [JsonPropertyName("deviceType")]
    public string DeviceType { get; set; }

    [JsonPropertyName("isTestSign")]
    public bool IsTestSign { get; set; }

    [JsonPropertyName("isFlightSign")]
    public bool IsFlightSign { get; set; }

    [JsonPropertyName("marketingNames")]
    public List<string> MarketingNames { get; set; }

    [JsonPropertyName("selectedProductTypes")]
    public Dictionary<string, string> SelectedProductTypes { get; set; }

    [JsonPropertyName("requestedSignatures")]
    public List<string> RequestedSignatures { get; set; }

    [JsonPropertyName("additionalAttributes")]
    public AdditionalAttributes AdditionalAttributes { get; set; }

    [JsonPropertyName("packageType")]
    public string PackageType { get; set; }
}

/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class PublishingSpecifications
{
    [JsonPropertyName("goLiveDate")]
    public DateTime GoLiveDate { get; set; }

    [JsonPropertyName("visibleToAccounts")]
    public List<long> VisibleToAccounts { get; set; }

    [JsonPropertyName("isAutoInstallDuringOSUpgrade")]
    public bool IsAutoInstallDuringOSUpgrade { get; set; }

    [JsonPropertyName("isAutoInstallOnApplicableSystems")]
    public bool IsAutoInstallOnApplicableSystems { get; set; }

    [JsonPropertyName("manualAcquisition")]
    public bool ManualAcquisition { get; set; }

    [JsonPropertyName("isDisclosureRestricted")]
    public bool IsDisclosureRestricted { get; set; }

    [JsonPropertyName("publishToWindows10s")]
    public bool PublishToWindows10S { get; set; }

    [JsonPropertyName("additionalInfoForMsApproval")]
    public AdditionalInfoForMsApproval AdditionalInfoForMsApproval { get; set; }
}

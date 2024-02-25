/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.  
--*/

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi;

public class AdditionalInfoForMsApproval
{
    [JsonPropertyName("microsoftContact")]
    public string MicrosoftContact { get; set; }

    [JsonPropertyName("validationsPerformed")]
    public string ValidationsPerformed { get; set; }

    [JsonPropertyName("affectedOems")]
    public List<string> AffectedOems { get; set; }

    [JsonPropertyName("isRebootRequired")]
    public bool IsRebootRequired { get; set; }

    [JsonPropertyName("isCoEngineered")]
    public bool IsCoEngineered { get; set; }

    [JsonPropertyName("isForUnreleasedHardware")]
    public bool IsForUnreleasedHardware { get; set; }

    [JsonPropertyName("hasUiSoftware")]
    public bool HasUiSoftware { get; set; }

    [JsonPropertyName("businessJustification")]
    public string BusinessJustification { get; set; }
}

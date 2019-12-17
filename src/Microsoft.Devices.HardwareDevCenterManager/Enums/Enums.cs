/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public enum InServicePublishInfoOSEnum
    {
        TH,
        RS1,
        RS2,
        RS3,
        RS4,
        RS5,
        [Description("NineteenH1")]
        [Display(Name = "19H1")]
        RS6_19H1,
        // Keep this at the end
        Invalid
    }
}

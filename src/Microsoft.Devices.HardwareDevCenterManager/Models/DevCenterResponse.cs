/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license.  See LICENSE file in the project root for full license information.  
--*/
using System.Collections.Generic;

namespace Microsoft.Devices.HardwareDevCenterManager.DevCenterApi
{
    public class DevCenterResponse<T>
    {
        public DevCenterErrorDetails Error { get; set; }
        public List<T> ReturnValue { get; set; }
    }
}

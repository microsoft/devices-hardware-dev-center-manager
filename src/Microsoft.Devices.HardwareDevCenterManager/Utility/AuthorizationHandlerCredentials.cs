/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.Utility;

public class AuthorizationHandlerCredentials
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("clientId")]
    public string ClientId { get; set; }

    [JsonPropertyName("tenantId")]
    public string TenantId { get; set; }

    [JsonPropertyName("url")]
    public System.Uri Url { get; set; }

    [JsonPropertyName("urlPrefix")]
    public System.Uri UrlPrefix { get; set; }

    [JsonPropertyName("managedIdentityClientId")]
    public string ManagedIdentityClientId { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }
}

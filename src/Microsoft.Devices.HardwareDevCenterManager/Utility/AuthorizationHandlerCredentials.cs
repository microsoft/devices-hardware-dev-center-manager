/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.
--*/

using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace Microsoft.Devices.HardwareDevCenterManager.Utility;

public class AuthorizationHandlerCredentials
{
    // used for Azure
    [JsonPropertyName("authority")]
    public string Authority { get; set; }

    [JsonPropertyName("tenantId")]
    public string TenantId { get; set; }

    [JsonPropertyName("clientId")]
    public string ClientId { get; set; }

    [JsonPropertyName("managedIdentityClientId")]
    public string ManagedIdentityClientId { get; set; }

    [JsonPropertyName("scope")]
    public string Scope { get; set; }

    [JsonPropertyName("x509Certificate2")]
    public X509Certificate2 X509Certificate2 { get; set; }

    [JsonPropertyName("x509Certificate2Name")]
    public string X509Certificate2Name { get; set; }

    [JsonPropertyName("keyVaultUrl")]
    public string KeyVaultUrl { get; set; }


    // used for HDC
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("url")]
    public System.Uri Url { get; set; }

    [JsonPropertyName("urlPrefix")]
    public System.Uri UrlPrefix { get; set; }
}

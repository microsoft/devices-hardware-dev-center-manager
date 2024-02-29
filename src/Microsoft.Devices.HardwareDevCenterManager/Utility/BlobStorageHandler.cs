/*++
    Copyright (c) Microsoft Corporation. All rights reserved.

    Licensed under the MIT license. See LICENSE file in the project root for full license information.  
--*/

using Azure;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Devices.HardwareDevCenterManager.Utility;

public class BlobStorageHandler
{
    private readonly BlockBlobClient _blockBlobClient;

    /// <summary>
    /// Handles upload and download of files for HDC Azure Blob Storage URLs
    /// </summary>
    /// <param name="SASUrl">URL String to the blob</param>
    public BlobStorageHandler(string SASUrl)
    {
        _blockBlobClient = new BlockBlobClient(new Uri(SASUrl));
    }

    /// <summary>
    /// Uploads specified file to HDC Azure Storage
    /// </summary>
    /// <param name="filePath">Path to the file to upload to the Azure Blob URL</param>
    /// <returns>True if the upload succeeded</returns>
    public async Task Upload(string filePath)
    {
        try
        {
            using System.IO.FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);
            await _blockBlobClient.UploadAsync(fileStream, null, default);
        }
        catch (RequestFailedException rfe)
        {
            Console.WriteLine("{process} {method} - RequestFailedException error uploading blob. Error - {errorMessage}",
                nameof(BlobStorageHandler),
                nameof(Download),
                rfe.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("{process} {method} - Exception uploading blob. Error - {errorMessage}",
                nameof(BlobStorageHandler),
                nameof(DownloadToString),
                ex.Message);
        }

        return;
    }

    private void ReportProgress()
    {
        // todo: see if we can find a way to write progress using the Azure.Storage namespace
    }

    /// <summary>
    /// Downloads to specified file from HDC Azure Storage
    /// </summary>
    /// <param name="filepath">Path to the file to download to, from the Azure Blob URL</param>
    /// <returns>True if the download succeeded</returns>
    public async Task Download(string filePath)
    {
        try
        {
            await _blockBlobClient.DownloadToAsync(filePath);
        }
        catch (RequestFailedException rfe)
        {
            Console.WriteLine("{process} {method} - RequestFailedException error downloading blob to file. Error - {errorMessage}",
                nameof(BlobStorageHandler),
                nameof(Download),
                rfe.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("{process} {method} - Exception downloading blob to file. Error - {errorMessage}",
                nameof(BlobStorageHandler),
                nameof(DownloadToString),
                ex.Message);
        }

        return;
    }

    /// <summary>
    /// Downloads a specified file from HDC Azure Storage as a string
    /// </summary>
    /// <returns>String representing the content from Azure Storage</returns>
    public async Task<string> DownloadToString()
    {
        try
        {
            Response<BlobDownloadResult> response = await _blockBlobClient.DownloadContentAsync();
            if (response != null)
            {
                if (response.Value != null)
                {
                    if (response.Value.Content != null)
                    {
                        return response.Value.Content.ToString();
                    }
                }
            }
        }
        catch (RequestFailedException rfe)
        {
            Console.WriteLine("{process} {method} - RequestFailedException error downloading blob to string. Error - {errorMessage}",
                nameof(BlobStorageHandler),
                nameof(DownloadToString),
                rfe.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("{process} {method} - Exception downloading blob to string. Error - {errorMessage}",
                nameof(BlobStorageHandler),
                nameof(DownloadToString),
                ex.Message);
        }

        return string.Empty;
    }
}

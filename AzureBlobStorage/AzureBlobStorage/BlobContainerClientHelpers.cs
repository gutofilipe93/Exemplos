using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobStorage
{
    public static class BlobContainerClientHelpers
    {
        public static async Task<BlobContainerClient> CreateBlobContainerClient(string storageConnectionString, string containerName)
        {
            var blobContainerClient = new BlobContainerClient(storageConnectionString, containerName);

            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            return blobContainerClient;
        }
    }
}

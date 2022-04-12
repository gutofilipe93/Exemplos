using Azure.Storage.Blobs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobStorage
{
    public static class BlobContainerClientExtensions
    {
        public static async Task SaveData(this BlobContainerClient blobContainerClient, string fileName, object data)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DateFormatString = "yyyy-MM-ddTHH:mm:sszzz"
                }));
                
                using var ms = new MemoryStream(bytes);
                _ = await blobContainerClient.UploadBlobAsync(fileName, ms);

                
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}

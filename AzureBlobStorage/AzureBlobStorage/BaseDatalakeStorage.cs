using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBlobStorage
{
    public abstract class BaseDatalakeStorage
    {
        protected string storageConnectionString;
        private string containerName;
        protected BaseDatalakeStorage() { }

        protected BaseDatalakeStorage(string containerName,
             string storageConnectionString)
        {
            SetContainerName(containerName);
            this.storageConnectionString = storageConnectionString;
        }
        protected void SetContainerName(string containerName)
        {
            this.containerName = containerName;
        }

        public async Task Save<T>(T data, string path, string fileName = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Parâmetro obrigatório não informado", nameof(fileName));
            }

            BlobContainerClient blobContainerClient = await BlobContainerClientHelpers.CreateBlobContainerClient(this.storageConnectionString, this.containerName);
            await blobContainerClient.CreateIfNotExistsAsync();

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = $"{Guid.NewGuid()}.json";
            }

            fileName = $"{path}/{fileName}";
            await blobContainerClient.SaveData(fileName, data);
        }
    }
}

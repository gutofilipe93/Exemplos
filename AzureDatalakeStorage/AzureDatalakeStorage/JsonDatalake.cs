using Azure.Storage;
using Azure.Storage.Files.DataLake;
using Azure.Storage.Files.DataLake.Models;
using AzureDatalakeStorage.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatalakeStorage
{
    public class JsonDatalake
    {
        private readonly DataLakeServiceClient _adlsClient;

        public JsonDatalake(string accountName, string accountKey)
        {
            StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(accountName, accountKey);

            string dfsUri = $"https://{accountName}.dfs.core.windows.net";
            _adlsClient = new DataLakeServiceClient(new Uri(dfsUri), sharedKeyCredential);
        }

        public async Task Save(Person person)
        {
            string directoryPath = "teste/pessoa";
            var fileSystemClient = _adlsClient.GetFileSystemClient("NomeConteiner");
            await fileSystemClient.CreateIfNotExistsAsync();

            var directoryClient = fileSystemClient.GetDirectoryClient(directoryPath);
            await directoryClient.CreateIfNotExistsAsync();

            var result = await directoryClient.CreateFileAsync($"{DateTime.Now.ToString("ddMMyyyy_HHmmssfff")}.json");
            var content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));

            using (var ms = new MemoryStream(content))
                await result.Value.UploadAsync(ms, overwrite: true);
        }

        //Buscar um arquivo do datalake pelo seu nome
        public async Task<Person> GetMessageAsync()
        {
            string directoryPath = "teste/pessoa";
            var fileSystemClient = _adlsClient.GetFileSystemClient("NomeConteiner");
            var directoryClient = fileSystemClient.GetDirectoryClient(directoryPath);

            var fileClient = directoryClient.GetFileClient($"teste.json");
            if (!await fileClient.ExistsAsync())
                return null;

            var response = await fileClient.ReadAsync();

            var fileDownloadInfo = response.Value;
            using (var sr = new StreamReader(fileDownloadInfo.Content))
            {
                var content = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<Person>(content);
            }
        }


        //Ler todos os arquivos de uma pasta do datalake
        public async Task<List<Person>> GetMessagesAsync()
        {
            string directoryPath = "teste/pessoa";
            var fileSystemClient = _adlsClient.GetFileSystemClient("NomeConteiner");
            IAsyncEnumerator<PathItem> enumerator =
            fileSystemClient.GetPathsAsync(directoryPath).GetAsyncEnumerator();
            try
            {
                await enumerator.MoveNextAsync();
            }
            catch
            {
                return new List<Person>();
            }

            PathItem item = enumerator.Current;
            List<Person> people = new List<Person>();
            while (item != null)
            {
                var fileClient = fileSystemClient.GetFileClient(item.Name);
                var response = await fileClient.ReadAsync();
                var fileDownloadInfo = response.Value;
                using (var sr = new StreamReader(fileDownloadInfo.Content))
                {
                    var content = await sr.ReadToEndAsync();
                    var data = JsonConvert.DeserializeObject<List<Person>>(content);
                    people = people.Union(data).ToList();
                }
                if (!await enumerator.MoveNextAsync())
                    break;

                item = enumerator.Current;
            }
            return people;
        }

        // Deleta a pasta
        public async Task DeleteFolder(string path)
        {
            var fileSystemClient = _adlsClient.GetFileSystemClient("NomeConteiner");
            var directoryClient = fileSystemClient.GetDirectoryClient(path);
            await directoryClient.DeleteIfExistsAsync();
        }

        // Deleta somente um arquivo
        public async Task Delete()
        {

            string directoryPath = "teste/pessoa";
            var fileSystemClient = _adlsClient.GetFileSystemClient("NomeConteiner");
            var directoryClient = fileSystemClient.GetDirectoryClient(directoryPath);
            var fileClient = directoryClient.GetFileClient($"teste.json");
            if (!await fileClient.ExistsAsync())
                return;

            await fileClient.DeleteAsync();
        }
    }
}

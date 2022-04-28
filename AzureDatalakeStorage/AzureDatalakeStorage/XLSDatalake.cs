using Azure.Storage;
using Azure.Storage.Files.DataLake;
using Azure.Storage.Files.DataLake.Models;
using AzureDatalakeStorage.Class;
using IronXL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatalakeStorage
{
    public class XLSDatalake
    {
        private readonly DataLakeServiceClient _adlsClient;

        public XLSDatalake(string accountName, string accountKey)
        {
            StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(accountName, accountKey);

            string dfsUri = $"https://{accountName}.dfs.core.windows.net";
            _adlsClient = new DataLakeServiceClient(new Uri(dfsUri), sharedKeyCredential);
        }

        public async Task SaveCollectionAsync(List<Person> people)
        {
            var fileSystemClient = _adlsClient.GetFileSystemClient("NomeConteiner");
            await fileSystemClient.CreateIfNotExistsAsync();
            string directoryPath = "teste/pessoa";
            var directoryClient = fileSystemClient.GetDirectoryClient(directoryPath);
            await directoryClient.CreateIfNotExistsAsync();
            var fileName = $"teste.xls";
            CreateFileXls(people, fileName);
            var result = await directoryClient.CreateFileAsync($"{fileName}.xls");
            await result.Value.UploadAsync(Path.GetTempPath() + fileName, overwrite: true);
        }

        private void CreateFileXls(List<Person> people, string fileName)
        {
            WorkBook workbook = WorkBook.Create(ExcelFileFormat.XLS);
            var sheet = workbook.CreateWorkSheet($"example_sheet_{fileName}");
            sheet[$"A1"].Value = "ID";
            sheet[$"B1"].Value = "NOME";
            sheet[$"C1"].Value = "DATA";            
            var count = 2;
            foreach (var person in people)
            {
                sheet[$"A{count}"].Value = person.Id;
                sheet[$"B{count}"].Value = person.Name;
                sheet[$"C{count}"].Value = person.Date;                
                count++;
            }
            workbook.SaveAs(Path.GetTempPath() + fileName);
        }

        public async Task<List<Person>> GetCollectionAsync()
        {
            string directoryPath = "teste/pessoa";
            var fileSystemClient = _adlsClient.GetFileSystemClient("NomeConteiner");
            IAsyncEnumerator<PathItem> enumerator =
            fileSystemClient.GetPathsAsync(directoryPath).GetAsyncEnumerator();
            try
            {
                await enumerator.MoveNextAsync();
            }
            catch (Exception ex)
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
                people = people.Union(ReadFileXls(fileDownloadInfo.Content)).ToList();
                if (!await enumerator.MoveNextAsync())
                {
                    break;
                }
                item = enumerator.Current;
            }
            return people;
        }

        private List<Person> ReadFileXls(Stream stream)
        {
            WorkBook workbook = WorkBook.LoadExcel(stream);
            WorkSheet sheet = workbook.WorkSheets.First();
            DataSet dataSet = workbook.ToDataSet();
            DataTable firstTable = dataSet.Tables[0];
            var count = 2;
            List<Person> people = new List<Person>();
            for (int i = 1; i < firstTable.Rows.Count; i++)
            {
                people.Add(new Person
                {
                    Id = sheet[$"A{count}"].Int32Value,
                    Name = sheet[$"B{count}"].StringValue,
                    Date = sheet[$"C{count}"].DateTimeValue ?? DateTime.Now,
                }) ;                
                count++;
            }

            return people;
        }
    }
}

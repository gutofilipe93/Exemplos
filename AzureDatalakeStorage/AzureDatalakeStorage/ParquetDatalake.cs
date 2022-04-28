using Azure.Storage;
using Azure.Storage.Files.DataLake;
using AzureDatalakeStorage.Class;
using Parquet;
using Parquet.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureDatalakeStorage
{
    public class ParquetDatalake
    {
        private readonly DataLakeServiceClient _adlsClient;

        public ParquetDatalake(string accountName, string accountKey)
        {
            StorageSharedKeyCredential sharedKeyCredential = new StorageSharedKeyCredential(accountName, accountKey);

            string dfsUri = $"https://{accountName}.dfs.core.windows.net";
            _adlsClient = new DataLakeServiceClient(new Uri(dfsUri), sharedKeyCredential);
        }


        public async Task Save( List<Person> people)
        {
            var fileSystemClient = _adlsClient.GetFileSystemClient("NomeConteiner");
            await fileSystemClient.CreateIfNotExistsAsync();
            string directoryPath = "teste/pessoa";
            var directoryClient = fileSystemClient.GetDirectoryClient(directoryPath);
            await directoryClient.CreateIfNotExistsAsync();
            var data = GenerateTestData(people);
            var fields = GenerateSchema(data);
            var fileName = $"{Guid.NewGuid()}.parquet";
            SaveFileParquetLocal(data, fields, fileName, people.Count());
            var result = await directoryClient.CreateFileAsync(fileName);
            await result.Value.UploadAsync(Path.GetTempPath() + fileName, overwrite: true);
        }

        private DataTable GenerateTestData(List<Person> people)
        {
            var dt = new DataTable("mkt_statusmensagem");
            dt.Columns.AddRange(new[]
            {
                new System.Data.DataColumn("Id", typeof(int)),
                new System.Data.DataColumn("Name", typeof(string)),
                new System.Data.DataColumn("Date", typeof(DateTime)),
            });

            foreach (var person in people)
                dt.Rows.Add(person.Id, person.Name, person.Date);

            return dt;
        }

        private List<DataField> GenerateSchema(DataTable dt)
        {
            var fields = new List<DataField>(dt.Columns.Count);

            foreach (System.Data.DataColumn column in dt.Columns)
            {
                // Attempt to parse the type of column to a parquet data type
                var success = Enum.TryParse<DataType>(column.DataType.Name, true, out var type);

                // If the parse was not successful and it's source is a DateTime then use a DateTimeOffset, otherwise default to a string
                if (!success && column.DataType == typeof(DateTime))
                {
                    type = DataType.DateTimeOffset;
                }
                else if (!success)
                {
                    type = DataType.String;
                }

                fields.Add(new DataField(column.ColumnName, type));
            }

            return fields;
        }

        private void SaveFileParquetLocal(DataTable dt, List<DataField> fields, string fileName, int count)
        {
            int RowGroupSize = count;
            using (var stream = File.Open(Path.GetTempPath() + fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new ParquetWriter(new Schema(fields), stream))
                {
                    var startRow = 0;
                    while (startRow < dt.Rows.Count)
                    {
                        using (var rgw = writer.CreateRowGroup())
                        {
                            for (var i = 0; i < dt.Columns.Count; i++)
                            {
                                var columnIndex = i;
                                var targetType = dt.Columns[columnIndex].DataType;
                                if (targetType == typeof(DateTime)) targetType = typeof(DateTimeOffset);

                                var valueType = targetType.IsClass
                                    ? targetType
                                    : typeof(Nullable<>).MakeGenericType(targetType);

                                var list = (IList)typeof(List<>)
                                    .MakeGenericType(valueType)
                                    .GetConstructor(Type.EmptyTypes)
                                    .Invoke(null);

                                foreach (var row in dt.AsEnumerable().Skip(startRow).Take(RowGroupSize))
                                {
                                    if (row[columnIndex] == null || row[columnIndex] == DBNull.Value)
                                    {
                                        list.Add(null);
                                    }
                                    else
                                    {
                                        list.Add(dt.Columns[columnIndex].DataType == typeof(DateTime)
                                            ? new DateTimeOffset((DateTime)row[columnIndex])
                                            : row[columnIndex]);
                                    }
                                }

                                var valuesArray = Array.CreateInstance(valueType, list.Count);
                                list.CopyTo(valuesArray, 0);

                                rgw.WriteColumn(new Parquet.Data.DataColumn(fields[i], valuesArray));

                            }
                        }

                        startRow += RowGroupSize;
                    }
                }
            }
        }


        //lendo somente um arquico pelo seu nome
        public async Task<List<Person>> Get()
        {

            string directoryPath = "teste/pessoa";

            var fileSystemClient = _adlsClient.GetFileSystemClient("NomeConteiner");
            var directoryClient = fileSystemClient.GetDirectoryClient(directoryPath);

            var fileClient = directoryClient.GetFileClient($"{Guid.NewGuid()}.parquet");
            if (!await fileClient.ExistsAsync())
                return null;

            var response = await fileClient.ReadAsync();            
            var fileDownloadInfo = response.Value;

            return ReadParquet(fileDownloadInfo.Content);
        }

        private List<Person> ReadParquet(Stream stream)
        {
            var people = new List<Person>();
            Person person;
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                ms.Position = 0;
                using (var parquetReader = new ParquetReader(ms))
                {
                    DataField[] dataFields = parquetReader.Schema.GetDataFields();
                    for (int i = 0; i < parquetReader.RowGroupCount; i++)
                    {
                        using (ParquetRowGroupReader groupReader = parquetReader.OpenRowGroupReader(i))
                        {
                            Parquet.Data.DataColumn[] columns = dataFields.Select(groupReader.ReadColumn).ToArray();
                            var ids = columns[0].Data.OfType<int>().ToArray();
                            var names = columns[1].Data.OfType<string>().ToArray();
                            var dates = columns[2].Data.OfType<DateTime>().ToArray();
                            for (int y = 0; y < groupReader.RowCount; y++)
                            {
                                person = new Person
                                {
                                    Id = ids[y],
                                    Name = names[y],
                                    Date = dates[y]
                                };
                                people.Add(person);
                            }
                        }
                    }
                }
            }
            return people;
        }

    }
}

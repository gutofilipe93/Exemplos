using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTableStorage
{
    public class AzureTableStoregeHelper
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTableClient _tableClient;
        private const string TABLE_NAME = "NAME_TABLE";
        public AzureTableStoregeHelper(string storageConnectionString)
        {
            _storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            _tableClient = _storageAccount.CreateCloudTableClient();
        }

        public async Task SaveMessage(EntityAzureTableStorage azureTableStorage)
        {
            var table = _tableClient.GetTableReference(TABLE_NAME);

            if (!await table.ExistsAsync())
                await table.CreateAsync();

            azureTableStorage.PartitionKey = "CHAVE_PRIMARIO_DA_TABELA";
            TableOperation insertOperation = TableOperation.Insert(azureTableStorage);
            await table.ExecuteAsync(insertOperation);
        }
        public async Task<List<EntityAzureTableStorage>> GetMessages(int clientId, DateTime date)
        {
            var table = _tableClient.GetTableReference(TABLE_NAME);
            var filterString = BuildFilterString(clientId, date, null);
            var tableQuery = new TableQuery<EntityAzureTableStorage>()
                .Where(filterString);

            var continuationToken = new TableContinuationToken();
            var queryResult = await table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);

            return queryResult.ToList();
        }

        private static string BuildFilterString(int id, DateTime date,string name)
        {
            string query = string.Empty;
            query = ComposeQuery(query, TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id.ToString()));
            query = ComposeQuery(query, TableQuery.GenerateFilterConditionForDate("Date", QueryComparisons.LessThanOrEqual, date));            
            query = ComposeQuery(query, TableQuery.GenerateFilterCondition("Name", QueryComparisons.Equal, name));

            return query;
        }

        private static string ComposeQuery(string query, string statement)
        {
            if (string.IsNullOrEmpty(query))
                return $"{statement}";
            else
                return $"{query} and {statement}";
        }

        public async Task Delete(EntityAzureTableStorage azureTableStorage)
        {
            var table = _tableClient.GetTableReference(TABLE_NAME);
            var filterString = BuildFilterString(azureTableStorage.ID, azureTableStorage.Date,azureTableStorage.Name);
            var tableQuery = new TableQuery<EntityAzureTableStorage>()
                .Where(filterString);

            var continuationToken = new TableContinuationToken();
            var queryResult = await table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);
            var batchOperation = new TableBatchOperation();
            batchOperation.Delete(queryResult.FirstOrDefault());
            await table.ExecuteBatchAsync(batchOperation);
        }

    }
}

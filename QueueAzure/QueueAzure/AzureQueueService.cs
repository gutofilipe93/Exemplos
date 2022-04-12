using Azure.Storage.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAzure
{
    public class AzureQueueService 
    {
        private string _queueConnectionString;

        public AzureQueueService(string queueConnectionString)
        {
            _queueConnectionString = queueConnectionString;
        }

        public async Task Enqueue<T>(string queueName, T message)
        {
            var queueClient = new QueueClient(_queueConnectionString, queueName);
            await queueClient.CreateIfNotExistsAsync();

            var messageToSend = Convert.ToBase64String(System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(message));

            await queueClient.SendMessageAsync(messageToSend);
        }

        public async Task EnqueueString(string queueName, string message)
        {
            var queueClient = new QueueClient(_queueConnectionString, queueName);
            await queueClient.CreateIfNotExistsAsync();

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(message);
            var messageToSend = Convert.ToBase64String(plainTextBytes);

            await queueClient.SendMessageAsync(messageToSend);
        }

        public void SetConnectionString(string connectionString)
        {
            _queueConnectionString = connectionString;
        }


    }
}

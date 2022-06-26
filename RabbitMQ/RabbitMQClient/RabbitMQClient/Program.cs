using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQClient
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            MessageInputModel message = new MessageInputModel
            {
                FromId = 1,
                Told = 2,
                Content = "contando tudo"                
            };

            ConnectionFactory _factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                     queue: "messages",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

                    var stringfiedMessage = JsonConvert.SerializeObject(message);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "messages",
                        basicProperties: null,
                        body: bytesMessage);
                }
            }                     
        }
    }
}

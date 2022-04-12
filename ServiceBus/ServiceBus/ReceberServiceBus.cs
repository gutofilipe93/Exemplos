using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public class ReceberServiceBus
    {
        private ServiceBusClient _serviceBusClient;
        private string _connectionString;
        public ReceberServiceBus(string connectionString)
        {
            _serviceBusClient = new ServiceBusClient(connectionString);
            _connectionString = connectionString;
        }

        public async Task DeleteDeadLettersAsync(string topicName, string subscriptionName)
        {
            var messageReceiver = new MessageReceiver(_connectionString, EntityNameHelper.FormatSubscriptionPath(topicName, subscriptionName), ReceiveMode.PeekLock);
            var message = await messageReceiver.ReceiveAsync();
            while ((message = await messageReceiver.ReceiveAsync()) != null)
            {
                await messageReceiver.CompleteAsync(message.SystemProperties.LockToken);
            }
            await messageReceiver.CloseAsync();
        }

        //Envia para o serviceBus 
        public async Task<SendMessageResponse> SendAsync(string topicName, string message)
        {
            try
            {
                ServiceBusSender sender = _serviceBusClient.CreateSender(topicName);
                await sender.SendMessageAsync(new ServiceBusMessage(message));
                return new SendMessageResponse { Message = "Mensagem enviada", Success = true };
            }
            catch
            {
                return new SendMessageResponse { Message = "Problemas ao enviar um evento para service bus", Success = false };
            }
        }
    }
}

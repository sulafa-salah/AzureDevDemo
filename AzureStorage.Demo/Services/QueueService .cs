﻿using Azure.Storage.Queues;
using AzureStorage.Demo.Models;
using AzureStorage.Demo.Services.IServices;
using Newtonsoft.Json;

namespace AzureStorage.Demo.Services
{
    public class QueueService : IQueueService
    {
        private readonly IConfiguration _configuration;
        private readonly QueueClient _queueClient;
        //private string queueName = "attendee-emails";

        public QueueService(IConfiguration configuration, QueueClient queueClient)
        {
            this._configuration = configuration;
            this._queueClient = queueClient;
        }

        public async Task SendMessage(EmailMessage emailMessage)
        {
            //var queueClient = new QueueClient(_configuration["ConnectionString"],
            //    queueName,
            //    new QueueClientOptions
            //    {
            //        MessageEncoding = QueueMessageEncoding.Base64
            //    });

            await _queueClient.CreateIfNotExistsAsync();

            var message = JsonConvert.SerializeObject(emailMessage);

            await _queueClient.SendMessageAsync(message);
        }
    }
}
 

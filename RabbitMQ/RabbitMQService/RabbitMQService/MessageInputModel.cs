﻿using System;
namespace RabbitMQService
{
    public class MessageInputModel
    {
        public int FromId { get; set; }
        public int Told { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt{get; set; } = DateTime.Now;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBus
{
    public class SendMessageResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InjecaoDependencia
{
    public class SmsSendService : ISendService
    {
        public string Send()
        {
            return "Envio de sms";
        }
    }
}
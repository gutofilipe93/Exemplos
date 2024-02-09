using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InjecaoDependencia
{
    public class EmailSendService : ISendService
    {
        public string Send()
        {
            return "Email enviado";
        }
    }
}
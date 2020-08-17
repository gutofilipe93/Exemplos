using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Integracao
{
    public interface IAllInClient
    {
        string GetTokenAsync(string user, string password);
        bool SendEmailLote(string token, List<PersonMessage> personMessages);
        bool SendEmailTransactional(string token, PersonMessage personMessages);
    }
}

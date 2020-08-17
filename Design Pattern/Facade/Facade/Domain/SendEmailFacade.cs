using Facade.Class;
using Facade.Integracao;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Domain
{
    public class SendEmailFacade : ISendEmailFacade
    {
        private readonly IAllInClient _allInClient;
        public SendEmailFacade(IAllInClient allInClient)
        {
            _allInClient = allInClient;
        }
        public bool SendEmailTransactionalAllIn(ParameterList parameterList, PersonMessage personMessage)
        {
            var token = _allInClient.GetTokenAsync(parameterList.user, parameterList.password);
            var result = _allInClient.SendEmailTransactional(token, personMessage);
            return result;
        }
    }
}

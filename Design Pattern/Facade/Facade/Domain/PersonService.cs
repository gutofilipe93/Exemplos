using Facade.Class;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.Domain
{
    public class PersonService
    {
        private readonly ISendEmailFacade _sendEmail;

        public PersonService(ISendEmailFacade sendEmail)
        {
            _sendEmail = sendEmail;
        }

        public string SendPersonMessage(ParameterList parameterList, PersonMessage personMessage)
        {
            bool result = _sendEmail.SendEmailTransactionalAllIn(parameterList, personMessage);
            return result ? "Email enviado com sucesso" : "Erro ao enviar email";
        }
    }
}

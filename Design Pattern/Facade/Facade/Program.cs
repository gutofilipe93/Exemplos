using Facade.Class;
using Facade.Domain;
using Facade.Integracao;
using System;

namespace Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            var parameterList = new ParameterList
            {
                user = "gutofilipe",
                password = "45@rederJ"
            };

            var personMessage = new PersonMessage
            {
                Email = "gustavo@gmail.com",
                Message = "Teste com o pattern Facade"
            };

            var personService = new PersonService(new SendEmailFacade(new AllInClient()));
            var result = personService.SendPersonMessage(parameterList, personMessage);
            Console.WriteLine(result);
        }
    }
}

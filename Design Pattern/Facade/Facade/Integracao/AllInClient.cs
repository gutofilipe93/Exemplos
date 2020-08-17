using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facade.Integracao
{
    public class AllInClient : IAllInClient
    {

        public string GetTokenAsync(string user, string password)
        {
            return $"{user}:{password}";
        }

        public bool SendEmailLote(string token, List<PersonMessage> personMessages)
        {
            Console.WriteLine($"Pegar o token: {token}, e fazer uma requisção para api do terceito");
            foreach (var message in personMessages)
            {
                Console.WriteLine($"Mensagens: {message.Message} - Email: {message.Email}");
            }
            return true;
        }

        public bool SendEmailTransactional(string token, PersonMessage personMessages)
        {
            Console.WriteLine($"Pegar o token: {token}, e fazer uma requisção para api do terceito");
            Console.WriteLine($"Mensagens: {personMessages.Message} - Email: {personMessages.Email}");
            return true;
        }
    }
}

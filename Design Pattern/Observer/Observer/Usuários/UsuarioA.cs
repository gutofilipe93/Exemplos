using System;
using System.Collections.Generic;
using System.Text;

namespace Observer.Usuários
{
    public class UsuarioA : IObserver
    {
        public void EnviarEmail()
        {
            Console.WriteLine("Email Recebido pelo usuário A");
        }
    }
}

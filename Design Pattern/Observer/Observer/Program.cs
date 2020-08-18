using Observer.Usuários;
using System;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            ISubject controladorEmail = new ControladorEmail();

            var usuarioA = new UsuarioA();
            var usuarioB = new UsuarioB();
            var usuarioC = new UsuarioC();

            controladorEmail.Adicionar(usuarioA);
            controladorEmail.Adicionar(usuarioB);
            controladorEmail.Adicionar(usuarioC);

            Console.WriteLine("Os usuarios A, B e C cadastraram-se para receber as promoções. \n");
     
            Console.WriteLine("Enviando os emails para os usuarios assinados (usuários cadastrados).\n");
     

            controladorEmail.EnviarEmail();

            Console.WriteLine("\nO usuário A resolveu concancelar a assinatura e não irá receber mais emails.\n");
     
            controladorEmail.Remover(usuarioA);

            Console.WriteLine("Enviando os emails para os usuarios assinados.\n");
            controladorEmail.EnviarEmail();

            Console.ReadKey();

            Console.ReadKey();
        }
    }
}

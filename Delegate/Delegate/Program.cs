using System;

/* No exemplo o uso de delegate foi para não haver replicação de código, pois todos os metodos (Mensagem1 ,Mensagem2, Mensagem3) fazem
a mesma coisa, o que muda neles é que cada um tem uma mensagem diferente, mas todos tem a mesma estruttura de apresentar essa mensagem na tela.
Então para não ter código repetido criou-se o delegate, onde atravez dele foi possivel passar uma metodo dentro de outro metodo*/
namespace Delegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Mensagem1();
            Mensagem2();
            Mensagem3();
        }

        static void Mensagem1()
        {
            string MinhaMensagem() => "Mensagem 1";

            var mensagemCompleta = ObterMensagem(MinhaMensagem);
            Console.WriteLine(mensagemCompleta);
        }

        static void Mensagem2()
        {
            string MinhaMensagem() => "Mensagem 2";

            var mensagemCompleta = ObterMensagem(MinhaMensagem);
            Console.WriteLine(mensagemCompleta);
        }

        static void Mensagem3()
        {
            string MinhaMensagem() => "Mensagem 3";

            var mensagemCompleta = ObterMensagem(MinhaMensagem);
            Console.WriteLine(mensagemCompleta);
        }

        public delegate string ObterMensagemEspecifica();

        static string ObterMensagem(ObterMensagemEspecifica mensagemEspecifica)
        {
            string mensagem = string.Empty;
            mensagem += "Início da mensagem\r\n\r\n";
            mensagem += mensagemEspecifica();
            mensagem += "\r\n\r\n";
            mensagem += "Final da mensagem\r\n";
            mensagem += "-----------------------------------------\r\n";

            return mensagem;
        }
    }
}

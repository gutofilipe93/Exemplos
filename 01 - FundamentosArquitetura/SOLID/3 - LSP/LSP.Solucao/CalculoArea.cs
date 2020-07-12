using System;

namespace SOLID.LSP.Solucao
{
    public class CalculoArea
    {
        private static void ObterAreaParalelogramo(Paralelogramo ret)
        {
            Console.Clear();
            Console.WriteLine("Calculo da área do Retangulo");
            Console.WriteLine();
            Console.WriteLine(ret.Altura + " * " + ret.Largura);
            Console.WriteLine();
            Console.WriteLine(ret.Area);
            Console.ReadKey();
        }

        public static void Calcular()
        {
            var quad = new Quadrado(5,5);
            var ret = new Retangulo(10, 5);

            ObterAreaParalelogramo(quad);
            ObterAreaParalelogramo(ret);
        }
    }
}

// Só se deve criar herança de uma classe quando a classe filho(Subclasse) poder ser substituivel por sua classe pai(SuperClasse)
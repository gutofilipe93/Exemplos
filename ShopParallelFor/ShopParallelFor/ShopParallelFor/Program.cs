using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopParallelFor
{
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingParallelForWithBreak();
        }

        //Break indica que nenhuma iteração após a iteração atual deve ser executada.
        //Ele cancela efetivamente qualquer iteração adicional do loop.
        //No entanto, ele não interrompe nenhuma iteração que já tenha começado a execução.
        //ShouldExitCurrentIteration -> quando chama o metodo Break esse atributo passa a ser TRUE
        private static void ShoppingParallelForWithBreak()
        {
            IEnumerable<int> number = FillParallelFor();
            Parallel.ForEach(number, (i, loopState) =>
            {
                
                if (i <= 50)
                {
                    Console.WriteLine("Caiu aqui!");
                    loopState.Break();
                    Console.WriteLine("Caiu aqui2!");
                }
                if (loopState.ShouldExitCurrentIteration)
                {
                    Console.WriteLine("Caiu aqui!3");
                    return;
                }
                Console.WriteLine("O número acrescentado é {0}", i);
            });
            Console.ReadKey();
        }

        //Chamar o método Stop indica que todas as iterações do loop que 
        //ainda não foram iniciadas não precisam ser executadas.Ele cancela efetivamente qualquer iteração adicional do loop.
        //No entanto, ele não interrompe nenhuma iteração que já tenha começado a execução.
        private static void ShoppingParallelForWithShop()
        {
            IEnumerable<int> number = FillParallelFor();
            Parallel.ForEach(number, (i, loopState) =>
            {
                
                if (i <= 50)
                {
                    Console.WriteLine("Caiu aqui!");
                    loopState.Stop();
                    Console.WriteLine("Caiu aqui2!");
                }
                if (loopState.IsStopped)
                {
                    return;
                }
                Console.WriteLine("O número acrescentado é {0}", i);
            });
            Console.ReadKey();
        }

        private static List<int> FillParallelFor()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 1001; i++)
            {
                list.Add(i);
            }
            return list;
        }
    }
}

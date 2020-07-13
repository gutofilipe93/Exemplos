using System;
using AbstractFactory.Solucao;
using AbstractFactory.Violacao;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            // Chamando as classe e os metodos com violação
            var con = new SqlConnection();
            con.Open();

            var cmd = new SqlCommand();
            cmd.Execute();

            Console.ReadKey();

            // Chamando as classe e os metodos com solução
            DbFactory db = new SqlFactory();

            var con1 = db.CreateConnection();
            con1.Open();

            var cmd1 = db.CreateCommand();
            cmd1.Execute();

            Console.ReadKey();
        }
    }
}

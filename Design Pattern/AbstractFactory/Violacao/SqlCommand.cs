using System;

namespace AbstractFactory.Violacao
{
    public class SqlCommand : DbCommand
    {
        public override void Execute()
        {
            Console.WriteLine("Executando o comando do sql.....");
        }
    }
}
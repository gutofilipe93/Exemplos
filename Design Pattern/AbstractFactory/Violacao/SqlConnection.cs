using System;

namespace AbstractFactory.Violacao
{
    public class SqlConnection : DbConnection
    {
        // Concret Product 
        public override void Open()
        {
            Console.WriteLine("Método Open de SQL Connection foi chamado...");
        }
    }
}
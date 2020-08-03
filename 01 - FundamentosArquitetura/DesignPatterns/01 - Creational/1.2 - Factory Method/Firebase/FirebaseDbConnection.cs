using DesignPatterns.FactoryMethod;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns._01___Creational._1._2___Factory_Method.Firebase
{
    public class FirebaseDbConnection : DbConnector
    {
        public FirebaseDbConnection(string connectionString) : base(connectionString)
        {
            ConnectionString = connectionString;
        }

        public override Connection Connect()
        {
            Console.WriteLine("Conectando ao banco Firebase...");
            var connection = new Connection(ConnectionString);
            connection.Open();

            return connection;
        }
    }
}

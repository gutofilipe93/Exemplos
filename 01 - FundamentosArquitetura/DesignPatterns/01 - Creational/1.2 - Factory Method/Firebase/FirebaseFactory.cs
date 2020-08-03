using DesignPatterns.FactoryMethod;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns._01___Creational._1._2___Factory_Method.Firebase
{
    public class FirebaseFactory : DbFactory
    {
        public override DbConnector CreateConnector(string connectionString)
        {
            return new FirebaseDbConnection(connectionString);
        }
    }
}

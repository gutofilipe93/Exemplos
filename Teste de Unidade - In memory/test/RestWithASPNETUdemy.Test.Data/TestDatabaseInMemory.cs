using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Test.Data
{
    public class TestDatabaseInMemory
    {
        public static MySQLContext GetDatabase(string name)
        {
            var inMemoryOptions = new DbContextOptionsBuilder<MySQLContext>()
                .UseInMemoryDatabase($"{name}_{Guid.NewGuid()}").Options;

            return new MySQLContext(inMemoryOptions);
        }

        public static MySQLContext GetDatabase()
        {
            var name = $"{Guid.NewGuid()}_{Guid.NewGuid()}";
            return GetDatabase(name);
        }
    }
}

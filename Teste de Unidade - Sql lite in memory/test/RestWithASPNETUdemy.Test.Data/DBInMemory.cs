using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestWithASPNETUdemy.Test.Data
{
    public class DBInMemory
    {
        private MySQLContext _context;

        public DBInMemory()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<MySQLContext>()
                .UseSqlite(connection)
                .EnableSensitiveDataLogging()
                .Options;

            _context = new MySQLContext(options);
            InsertFakeData();
        }

        public MySQLContext GetContext() => _context;

        private void InsertFakeData()
        {
            if (_context.Database.EnsureCreated())
            {
                _context.Books.Add(
                    new Book { Author = "Teste de unidade", Price = 20, Id = 1, Title = "Teste de unidade", LaunchDate = DateTime.Now}
                );
                _context.Books.Add(
                    new Book { Author = "Teste de unidade 2", Price = 20, Id = 2, Title = "Teste de unidade 2", LaunchDate = DateTime.Now.Subtract(TimeSpan.FromDays(1)) }
                );

                _context.SaveChanges();
            }
        }
    }
}

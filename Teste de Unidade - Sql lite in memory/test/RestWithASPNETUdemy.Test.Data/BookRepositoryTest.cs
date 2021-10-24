using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Generic;
using System;
using System.Linq;
using Xunit;

namespace RestWithASPNETUdemy.Test.Data
{
    public class BookRepositoryTest
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly MySQLContext _context;
        public BookRepositoryTest()
        {
            var dbInMemory = new DBInMemory();
            _context = dbInMemory.GetContext();
            _bookRepository = new GenericRepository<Book>(_context);
        }

        [Fact]
        public void BuscarTodosOsLivros()
        {
            //Act
            var books = _bookRepository.FindAll();

            //Assert
            Assert.Equal(2, books.Count());
        }

        [Fact]
        public void AdicionarUmLivro()
        {
            //Act
            var books = _bookRepository.Create(new Book { Author = "Teste de unidade 3", Price = 20, Id = 3, Title = "Teste de unidade 3", LaunchDate = DateTime.Now });
            
            //Assert
            Assert.Equal(3, _context.Books.Count());
        }
    }
}

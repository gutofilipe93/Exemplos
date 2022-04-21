using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Generic;
using System;
using System.Collections.Generic;
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
            _context = TestDatabaseInMemory.GetDatabase("TesteUnidadeInMemory");
            _bookRepository = new GenericRepository<Book>(_context);
        }
        

        [Fact]
        public void BuscarTodosOsLivros()
        {
            // Arrange
            var bookslist = new List<Book>()
            {
                new Book
                {
                    Id = 1,
                    Author = "teste1"
                },
                new Book
                {
                    Id = 2,
                    Author = "teste3"
                }               
            };
            _context.AddRange(bookslist);
            _context.SaveChanges();

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
            Assert.Equal(1, _context.Books.Count());
        }
    }
}

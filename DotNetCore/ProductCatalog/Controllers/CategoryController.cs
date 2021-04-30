using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories;

namespace ProductCatalog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;
        public CategoryController(CategoryRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/categories"), HttpGet]
        public IEnumerable<Category> Get()
        {
            return _repository.Get();
        }

        [Route("v1/categories/{id}"), HttpGet]
        public Category Get(int id)
        {
            return _repository.Get(id);
        }

        [Route("v1/categories/{id}/products"), HttpGet]
        public IEnumerable<Product> GetProducts(int id)
        {
            return _repository.GetProducts(id);
        }

        [Route("v1/categories"), HttpPost]
        public Category Post([FromBody] Category category)
        {
            _repository.Add(category);
            return category;
        }


        [Route("v1/categories"), HttpPut]
        public Category Put([FromBody] Category category)
        {
            _repository.Edit(category);
            return category;
        }

        [Route("v1/categories"), HttpDelete]
        public Category Delete([FromBody] Category category)
        {
            _repository.Delete(category);
            
            return category;
        }

    }
}
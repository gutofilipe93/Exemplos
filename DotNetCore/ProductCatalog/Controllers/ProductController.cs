using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;
        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/products"), HttpGet]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/products/{id}"), HttpGet]
        public Product Get(int id)
        {
            return _repository.Get(id);
        }

        [Route("v1/products"), HttpPost]
        public ResultViewModel Post([FromBody] EditorProductViewModel model)
        {

            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possivel cadastrar o produto",
                    Data = model.Notifications
                };
            }

            var product = new Product
            {
                Title = model.Title,
                Description = model.Description,
                Image = model.Image,
                Price = model.Price,
                CategoryId = model.CategoryId,
                CreateDate = DateTime.Now,
                LastUpdateDate = DateTime.Now,
                Quantity = model.Quantity
            };

            _repository.Add(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto cadastrado",
                Data = product
            };

        }

        [Route("v1/products"), HttpPut]
        public ResultViewModel Put([FromBody] EditorProductViewModel model)
        {

            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel
                {
                    Success = false,
                    Message = "Não foi possivel atualizar o produto",
                    Data = model.Notifications
                };
            }

            var product = _repository.Get(model.Id);

            product.Title = model.Title;
            product.Description = model.Description;
            product.Image = model.Image;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.LastUpdateDate = DateTime.Now;
            product.Quantity = model.Quantity;

            _repository.Edit(product);

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto atualizado",
                Data = product
            };

        }

    }
}
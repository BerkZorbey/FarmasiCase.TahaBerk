using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public void AddProduct(Product addProduct)
        {
            _repository.Add(addProduct);
        }
        
        public void DeleteProduct(Guid id)
        {
            _repository.Delete(id);
        }

        public async Task<List<Product>> GetAllProduct()
        {
            return await _repository.GetAllProducts();
        }

        public Task<Product> GetProductById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void UpdateProduct(Guid id, Product updateProduct)
        {
            _repository.Update(id, updateProduct);
        }
    }
}

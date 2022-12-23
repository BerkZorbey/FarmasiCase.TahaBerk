using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProduct();
        Task<Product> GetProductById(Guid id);
        void AddProduct(Product addProduct);
        void UpdateProduct(Guid id, Product updateProduct);
        void DeleteProduct(Guid id);
    }
}

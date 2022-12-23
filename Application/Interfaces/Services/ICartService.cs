using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface ICartService
    {
        Task<List<Cart>> GetAllCart();
        Task<Cart> GetCartById(Guid id);
        void AddCart(Cart addCart);
        void UpdateCart(Guid id, Cart updateCart);
        void DeleteCart(Guid id);
    }
}

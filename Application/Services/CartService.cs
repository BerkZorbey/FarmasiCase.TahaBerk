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
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        private readonly IUserRepository _userRepository;

        public CartService(ICartRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public void AddCart(Cart addCart)
        {
            _repository.Add(addCart);
        }

        public void DeleteCart(Guid id)
        {
            _repository.Delete(id);
        }

        public Task<List<Cart>> GetAllCart()
        {
            return _repository.GetAll();
        }

        public Task<Cart> GetCartById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void UpdateCart(Guid id, Cart updateCart)
        {
            _repository.Update(id, updateCart);
        }
        
    }
}

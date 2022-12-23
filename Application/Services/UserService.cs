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
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public void AddUser(User user)
        {
            _repository.Add(user);
        }

        public Task<User> GetUserById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void UpdateUser(Guid id,User user)
        {
            _repository.Update(id, user);
        }

    }
}

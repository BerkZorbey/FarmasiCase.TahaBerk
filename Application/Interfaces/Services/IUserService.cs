using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetUserById(Guid id);
        void AddUser(User user);
        void UpdateUser(Guid id, User user);
    }
}

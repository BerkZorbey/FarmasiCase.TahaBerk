using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        public UserRepository(IOptions<MongoDbSetting> options) : base(options)
        {
        }
    }
}

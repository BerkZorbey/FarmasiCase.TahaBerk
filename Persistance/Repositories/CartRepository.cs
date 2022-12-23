using Application.Interfaces.Repositories;
using Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class CartRepository : RedisRepository<Cart>, ICartRepository
    {
        public CartRepository(IConnectionMultiplexer redis) : base(redis)
        {
        }
    }
}

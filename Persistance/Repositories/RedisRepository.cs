using Application.Interfaces.Repositories;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Compressors.ADC;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class RedisRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        public RedisRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public async void Add(TEntity entity)
        {
            var id = entity.Id;
            var json = JsonSerializer.Serialize(entity);
            await _database.HashSetAsync("key", new HashEntry[] { new HashEntry(id.ToString(), json) });
        }

        public async void Delete(Guid id)
        {
            await _database.HashDeleteAsync("key", id.ToString());
        }

        public async Task<List<TEntity>> GetAll()
        {
            var getAll = await _database.HashGetAllAsync("key");

            if (getAll.Length > 0)
            {
                var obj = Array.ConvertAll(getAll, x => JsonSerializer.Deserialize<TEntity>(x.Value)).ToList();
                return obj;
            }

            return null;
        }

        public async Task<TEntity> GetById(Guid id)
        {
           
            var get = await _database.HashGetAsync("key",id.ToString());
            var json = JsonSerializer.Deserialize<TEntity>(get);
            return json;
        }

        public async void Update(Guid id, TEntity entity)
        {
            var update = await _database.HashGetAsync("key", id.ToString());
            if (!string.IsNullOrEmpty(update))
            {
                var json = JsonSerializer.Serialize(entity);
                await _database.HashSetAsync("key", new HashEntry[] { new HashEntry(id.ToString(), json) });
            }
        }
    }
}

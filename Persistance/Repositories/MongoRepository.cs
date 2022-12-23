using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IMongoCollection<TEntity> _collection;
        public MongoRepository(IOptions<MongoDbSetting> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<TEntity>(options.Value.CollectionName);
        }
        public async void Add(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async void Delete(Guid id)
        {
            await _collection.DeleteOneAsync(x=> x.Id == id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            var getAll = await _collection.FindAsync(x => x.Id != null).Result.ToListAsync();
            return getAll;
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _collection.FindAsync(x=>x.Id == id).Result.FirstOrDefaultAsync();        
        }

        public async void Update(Guid id, TEntity entity)
        {
            await _collection.ReplaceOneAsync(x=>x.Id == id, entity);
        }
    }
}

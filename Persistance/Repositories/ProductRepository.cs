using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class ProductRepository : MongoRepository<Product>, IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;
        public ProductRepository(IOptions<MongoDbSetting> options) : base(options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<Product>(options.Value.CollectionName);
        }
        public async Task<List<Product>> GetAllProducts()
        {
            
            var getAll = await _collection.FindAsync(x => x.Name != null).Result.ToListAsync();
            return getAll;
        }
    }
}

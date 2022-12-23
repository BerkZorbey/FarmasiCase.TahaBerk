using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        void Add(TEntity entity);
        void Update(Guid id, TEntity entity);
        void Delete(Guid id);
    }
}

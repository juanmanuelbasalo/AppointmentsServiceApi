using AppointmentsAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppointmentsAPI.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        TEntity Get(Guid id);
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        IEnumerable<TEntity> Find(Expression<Func<TEntity,bool>> searchTerm);
        Task<bool> SaveAsync();
    }
}

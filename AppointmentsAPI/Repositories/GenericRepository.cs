using AppointmentsAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppointmentsAPI.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppointmentsDbContext context;
        private DbSet<TEntity> entities;
        public GenericRepository(AppointmentsDbContext context) => this.context = context;
      
        private DbSet<TEntity> Entities => entities ?? (entities = context.Set<TEntity>());

        public void Delete(TEntity entity) => Entities.Remove(entity);
        public TEntity Get(Guid id) => Entities.AsNoTracking().FirstOrDefault(entity => entity.Id == id);
        public IQueryable<TEntity> GetAll() => Entities.AsNoTracking();
        public void Insert(TEntity entity) => Entities.Add(entity);
        public IEnumerable<TEntity> Find(Expression<Func<TEntity,bool>> searchTerm) => Entities.Where(searchTerm);
        public void Update(TEntity entity) => context.Update(entity);
        public async Task<bool> SaveAsync()
        {
            var result = await context.SaveChangesAsync();
            return result >= 0;
        }
    }
}

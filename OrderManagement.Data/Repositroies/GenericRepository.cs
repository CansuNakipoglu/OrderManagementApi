using Microsoft.EntityFrameworkCore;
using OrderManagement.Data.Contexts.Abstracts;
using OrderManagement.Data.Repositroies.Abstracts;
using System.Linq.Expressions;

namespace OrderManagement.Data.Repositroies
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;
        private readonly IDbContext _dbContext;

        public GenericRepository(IDbContext context)
        {
            dbSet = context.Set<T>();
            _dbContext = context;
        }

        public virtual EntityState Add(T entity)
        {
            return dbSet.Add(entity).State;
        }

        public virtual EntityState Delete(T entity)
        {
            return dbSet.Remove(entity).State;
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public async Task<T?> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual EntityState Update(T entity)
        {
            return dbSet.Update(entity).State;
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }
    }
}

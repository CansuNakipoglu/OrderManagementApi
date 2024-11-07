using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OrderManagement.Data.Repositroies.Abstracts
{
    public interface IGenericRepository<T>
    {
        EntityState Add(T entity);
        EntityState Delete(T entity);
        EntityState Update(T entity);
        Task<T?> GetAsync(int id);
        IQueryable<T?> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        bool Exists(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
    }
}

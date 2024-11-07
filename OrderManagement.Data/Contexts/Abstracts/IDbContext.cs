using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data.Contexts.Abstracts
{
    public interface IDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool confirmAllTransactions, CancellationToken cancellationToken);
        int SaveChanges();
        int SaveChanges(bool confirmAllTransactions);
        void Dispose();
    }
}

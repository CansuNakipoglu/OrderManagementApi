using OrderManagement.Data.Contexts.Abstracts;
using OrderManagement.Data.Repositroies.Abstracts;

namespace OrderManagement.Data.Repositroies
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}

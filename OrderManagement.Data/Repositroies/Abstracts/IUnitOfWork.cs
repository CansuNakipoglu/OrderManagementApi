namespace OrderManagement.Data.Repositroies.Abstracts
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}

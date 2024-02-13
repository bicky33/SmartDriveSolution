namespace Northwind.Domain.Repositories
{
    public interface IUnitOfWorks
    {
        Task<int> SaveChangesAsync();

    }
}

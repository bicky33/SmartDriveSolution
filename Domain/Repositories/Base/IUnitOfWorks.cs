namespace Domain.Repositories
{
    public interface IUnitOfWorks
    {
        Task<int> SaveChangesAsync();

    }
}

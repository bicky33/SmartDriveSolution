namespace Domain.Repositories.Base
{
    public interface IUnitOfWorks
    {
        Task<int> SaveChangesAsync();

    }
}

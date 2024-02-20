namespace Domain.Repositories.Master
{
    //extend dari iRepositoryentityBase
    public interface IRepositoryEntityBaseMaster<T> where T : class
    {
        Task<IEnumerable<T>> GetAllEntityMaster(bool trackChanges);

        Task<T> GetEntityByNameMaster(string name, bool trackChanges);

        void CreateEntityMaster(T entity);

        void DeleteEntityMaster(T entity);
    }
}
namespace Service.Abstraction.Master
{
    public interface IServiceEntityBaseMaster<T>
    {
        Task<IEnumerable<T>> GetAllAsyncMaster(bool trackChanges);

        Task<T> GetByNameAsyncMaster(string name, bool trackChanges);

        Task<T> CreateAsyncMaster(T entity);

        Task UpdateAsyncMaster(string name, T entity);

        Task DeleteAsyncMaster(string name);
    }
}
namespace Service.Abstraction.Base
{
    public interface IServiceEntityBaseMaster<T>
    {
        Task<IEnumerable<T>> GetAllAsyncMaster(bool trackChanges);

        Task<T> GetByNameAsyncMaster(string name, bool trackChanges);

        Task<T> CreateAsyncMaster(T entity);

        Task<T> UpdateAsyncMaster(string name, T entity);

        Task DeleteAsyncMaster(string name);
    }
}
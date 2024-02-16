namespace Service.Abstraction.Base
{
    public interface IServiceEntityBase<T>
    {
        Task<IEnumerable<T>> GetAllAsync(bool trackChanges);

        Task<T> GetByIdAsync(int id, bool trackChanges);

        Task<T> CreateAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
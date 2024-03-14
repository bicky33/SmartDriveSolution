namespace Domain.Repositories.SO
{
    public interface IRepositoryWithConditionSO<TEntity, TID> : IRepositorySOEntityBase<TEntity, TID> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllByAgentId(int id, bool trackChanges);
    }
}

namespace Domain.Repositories.SO
{
    public interface IRepositoryRelationSOBase<TEntity, TID> : IRepositorySOEntityBase<TEntity, TID> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllByRelation(string name, object value, bool trackChanges);
    }
}

using Domain.RequestFeatured;
using Domain.RequestFeatured.SO;

namespace Domain.Repositories.SO
{
    public interface IRepositoryWithPagingSO
    {
        public interface IRepositoryWithPagingSO<TEntity, TID> : IRepositoryWithConditionSO<TEntity, TID> where TEntity : class
        {
            Task<IEnumerable<TEntity>> GetWithPaging(EntityParameterSO entityParams, bool trackChanges);
        }
    }
}

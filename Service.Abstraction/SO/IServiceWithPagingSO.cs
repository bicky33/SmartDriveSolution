using Domain.RequestFeatured;
using Domain.RequestFeatured.SO;

namespace Service.Abstraction.SO
{
    public interface IServiceWithPagingSO<TEntityDto, TEntityDtoCreate, TID> : IServiceSOEntityBase<TEntityDto, TEntityDtoCreate, TID> where TEntityDto : class
    {
        Task<PagedListSO<TEntityDto>> GetAllWithPagingAsync(EntityParameterSO entityParams, bool trackChanges);
        Task<IEnumerable<TEntityDto>> GetAllByAgentId(int agentId,bool trackChanges);
    }
}
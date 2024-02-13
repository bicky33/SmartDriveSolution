using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Domain.Repositories.Base;
using Domain.Repositories.Payment;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Payment
{
    public class FintechService : IServiceEntityBase<FintechDto>
    {
        private readonly IRepositoryPaymentManager _repositoryManager;

        public FintechService(IRepositoryPaymentManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<FintechDto> CreateAsync(FintechDto entity)
        {
            //TODO create BussinessEntity first,
            //then apply it to bank id (fintech.entitiyId= BussinessEntityId)
            var fintech = entity.Adapt<Fintech>();
            _repositoryManager.FintechRepository.CreateEntity(fintech);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
            return fintech.Adapt<FintechDto>();
        }

        public Task DeleteAsync(int id)
        {
            //TODO remove bank first, then remove BussinessEntity 
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FintechDto>> GetAllAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<FintechDto> GetByIdAsync(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<FintechDto> UpdateAsync(int id, FintechDto entity)
        {
            throw new NotImplementedException();
        }
    }
}

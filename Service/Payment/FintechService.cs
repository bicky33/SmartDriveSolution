using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Domain.Exceptions;
using Domain.Repositories.Base;
using Domain.Repositories.Payment;
using Domain.Repositories.UserModule;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Payment
{
    public class FintechService : IServiceEntityBase<FintechDto>
    {
        private readonly IRepositoryPaymentManager _repositoryManager;
        private readonly IRepositoryManagerUser _repositoryManagerUser;

        public FintechService(IRepositoryPaymentManager repositoryManager, IRepositoryManagerUser repositoryManagerUser)
        {
            _repositoryManager = repositoryManager;
            _repositoryManagerUser = repositoryManagerUser;
        }

        public async Task<FintechDto> CreateAsync(FintechDto entity)
        {
            var bussinessEntity = _repositoryManagerUser.BusinessEntityRepository.CreateEntity();
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            entity.FintEntityid = bussinessEntity.Entityid;
            var fintech = entity.Adapt<Fintech>();
            _repositoryManager.FintechRepository.CreateEntity(fintech);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
            return fintech.Adapt<FintechDto>();
        }

        public async Task DeleteAsync(int id)
        {
            //TODO remove bank first, then remove BussinessEntity 
            var fintech = await _repositoryManager.FintechRepository.GetEntityById(id, true);
            if (fintech == null)
            {
                throw new EntityNotFoundException(id, "Fintech");
            }
            _repositoryManager.FintechRepository.DeleteEntity(fintech);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<FintechDto>> GetAllAsync(bool trackChanges)
        {
            var fintech = await _repositoryManager.FintechRepository.GetAllEntity(false);
            var fintechDto = fintech.Adapt<IEnumerable<FintechDto>>();

            return fintechDto;
        }

        public async Task<FintechDto> GetByIdAsync(int id, bool trackChanges)
        {
            var fintech = await _repositoryManager.FintechRepository.GetEntityById(id, false);
            if (fintech == null)
                throw new EntityNotFoundException(id, "Fintech");
            var dto = fintech.Adapt<FintechDto>();
            return dto;
        }

        public async Task UpdateAsync(int id, FintechDto entity)
        {
            var fintech = await _repositoryManager.FintechRepository.GetEntityById(id, true);
            if (fintech == null)
                throw new EntityNotFoundException(id, "Fintech");

            fintech.FintName = entity.FintName;
            fintech.FintDesc = entity.FintDesc;
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

        }
    }
}

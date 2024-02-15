using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Domain.Exceptions;
using Domain.Repositories.Payment;
using Domain.Repositories.UserModule;
using Mapster;
using Service.Abstraction.Base;
using Service.Abstraction.User;

namespace Service.Payment
{
    public class BankService : IServiceEntityBase<BankDto>
    {
        private readonly IRepositoryPaymentManager _repositoryManager;
        private readonly IRepositoryManagerUser _repositoryManagerUser;

        public BankService(IRepositoryPaymentManager repositoryManager, IRepositoryManagerUser repositoryManagerUser)
        {
            _repositoryManager = repositoryManager;
            _repositoryManagerUser = repositoryManagerUser;
        }

        public async Task<BankDto> CreateAsync(BankDto entity)
        {
            var bussinessEntity = _repositoryManagerUser
                .BusinessEntityRepository.CreateEntity();
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            var bank = entity.Adapt<Bank>();
            bank.BankEntityid = bussinessEntity.Entityid;
            _repositoryManager.BankRepository.CreateEntity(bank);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            return bank.Adapt<BankDto>();
        }

        public async Task DeleteAsync(int id)
        {
            //TODO remove bank first, then remove BussinessEntity 
            var category = await _repositoryManager.BankRepository.GetEntityById(id, false);
            if (category == null)
            {
                throw new EntityNotFoundException(id, "Bank");
            }
            _repositoryManager.BankRepository.DeleteEntity(category);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<BankDto>> GetAllAsync(bool trackChanges)
        {
            var categories = await _repositoryManager.BankRepository.GetAllEntity(trackChanges);
            var categoryDto = categories.Adapt<IEnumerable<BankDto>>();

            return categoryDto;
        }

        public async Task<BankDto> GetByIdAsync(int id, bool trackChanges)
        {
            var bank = await _repositoryManager.BankRepository.GetEntityById(id, trackChanges);

            if (bank == null)
                throw new EntityNotFoundException(id, "Bank");

            var dto = bank.Adapt<BankDto>();
            return dto;
        }

        public async Task UpdateAsync(int id, BankDto entity)
        {
            var category = await _repositoryManager.BankRepository.GetEntityById(id, true);

            if (category == null)
                throw new EntityNotFoundException(id, "Bank");

            category.BankName = entity.BankName;
            category.BankDesc = entity.BankDesc;
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

        }

    }
}

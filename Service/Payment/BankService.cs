using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Domain.Exceptions;
using Domain.Repositories.Payment;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Payment
{
    public class BankService : IServiceEntityBase<BankDto>
    {
        private readonly IRepositoryPaymentManager _repositoryManager;

        public BankService(IRepositoryPaymentManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<BankDto> CreateAsync(BankDto entity)
        {
            //TODO create BussinessEntity first,
            //then apply it to bank id (bank.entitiyId= BussinessEntityId)
            var bank = entity.Adapt<Bank>();
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
            var categories = await _repositoryManager.BankRepository.GetAllEntity(false);
            var categoryDto = categories.Adapt<IEnumerable<BankDto>>();

            return categoryDto;
        }

        public async Task<BankDto> GetByIdAsync(int id, bool trackChanges)
        {
            var categoy = await _repositoryManager.BankRepository.GetEntityById(id, false);

            if (categoy == null)
                throw new EntityNotFoundException(id, "Bank");

            var dto = categoy.Adapt<BankDto>();
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

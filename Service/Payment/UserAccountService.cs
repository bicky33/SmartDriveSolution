using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Repositories.Payment;
using Domain.Repositories.UserModule;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Payment
{
    public class UserAccountService : IServiceEntityBase<UserAccountDto>
    {
        private readonly IRepositoryPaymentManager _repositoryPaymentManager;
        private readonly IRepositoryManagerUser _repositoryManagerUser;

        public UserAccountService(IRepositoryPaymentManager repositoryPaymentManager, IRepositoryManagerUser repositoryManagerUser)
        {
            _repositoryPaymentManager = repositoryPaymentManager;
            _repositoryManagerUser = repositoryManagerUser;
        }

        public async Task<UserAccountDto> CreateAsync(UserAccountDto entity)
        {
            //var user = _repositoryManagerUser.UserRepository.GetEntityById(entity.UsacUserEntityid, false);
            //if (user == null)
            //    throw new EntityNotFoundException(entity.UsacUserEntityid, "User");

            //var bank = _repositoryPaymentManager.BankRepository.GetEntityById(entity.UsacBankEntityid, false);
            //if (bank == null)
            //    throw new EntityNotFoundException(entity.UsacUserEntityid, "Bank");

            //var fintech = _repositoryPaymentManager.FintechRepository.GetEntityById(entity.UsacFintEntityid, false);
            //if (fintech == null)
            //    throw new EntityNotFoundException(entity.UsacUserEntityid, "Fintech");

            var userAccount = entity.Adapt<UserAccount>();
            _repositoryPaymentManager.UserAccountRepository.CreateEntity(userAccount);
            await _repositoryPaymentManager.UnitOfWorks.SaveChangesAsync();
            return userAccount.Adapt<UserAccountDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var userAccount = await _repositoryPaymentManager.UserAccountRepository.GetEntityById(id, true);
            if (userAccount == null)
                throw new EntityNotFoundException(id, "UserAccount");

            _repositoryPaymentManager.UserAccountRepository.DeleteEntity(userAccount);
            await _repositoryPaymentManager.UnitOfWorks.SaveChangesAsync();

        }

        public async Task<IEnumerable<UserAccountDto>> GetAllAsync(bool trackChanges)
        {
            var userAccounts = await _repositoryPaymentManager.UserAccountRepository.GetAllEntity(trackChanges);
            var userAccountDto = userAccounts.Adapt<IEnumerable<UserAccountDto>>();

            return userAccountDto;
        }

        public async Task<UserAccountDto> GetByIdAsync(int id, bool trackChanges)
        {
            var userAccount = await _repositoryPaymentManager.UserAccountRepository.GetEntityById(id, trackChanges);
            if (userAccount == null)
                throw new EntityNotFoundException(id, "UserAccount");

            var userAccountDto = userAccount.Adapt<UserAccountDto>();
            return userAccountDto;
        }

        public async Task UpdateAsync(int id, UserAccountDto entity)
        {
            var userAccount = await _repositoryPaymentManager.UserAccountRepository.GetEntityById(id, true);
            if (userAccount == null)
                throw new EntityNotFoundException(id, "UserAccount");

            userAccount.UsacAccountno = entity.UsacAccountno;
            await _repositoryPaymentManager.UnitOfWorks.SaveChangesAsync();
        }

    }
}

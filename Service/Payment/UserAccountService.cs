using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.Repositories.Payment;
using Domain.Repositories.UserModule;
using Mapster;
using Service.Abstraction.Base;
using Service.Abstraction.Payment;

namespace Service.Payment
{
    public class UserAccountService : IServiceEntityUserAccount
    {
        private readonly IRepositoryPaymentManager _repositoryPaymentManager;
        private readonly IRepositoryManagerUser _repositoryManagerUser;

        public UserAccountService(IRepositoryPaymentManager repositoryPaymentManager, IRepositoryManagerUser repositoryManagerUser)
        {
            _repositoryPaymentManager = repositoryPaymentManager;
            _repositoryManagerUser = repositoryManagerUser;
        }

        public async Task<UserAccountDto> CreateAsync(AccountTypeEnum accountType, UserAccountCreateDto entity)
        {
            entity.UsacType = accountType;
            var userAccount = entity.Adapt<UserAccount>();
            userAccount.UsacDebet = 0;
            userAccount.UsacCredit = 0;
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

        public async Task<UserAccountDto> GetByAccountNoAsync(string id, bool trackChanges, ReturnException returnException)
        {
            var userAccount = await _repositoryPaymentManager.UserAccountRepository.GetUserAccountByAccountNo(id, trackChanges);
            if (userAccount == null && returnException == ReturnException.RETURN_WHEN_NULL)
                throw new EntityNotFoundException(id, "UserAccount");

            if (userAccount != null && returnException == ReturnException.RETURN_WHEN_EXIST)
                throw new EntityAlreadyExistException(id, "UserAccount");

            var userAccountDto = userAccount.Adapt<UserAccountDto>();
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

        public async Task<IEnumerable<UserAccountDto>> GetAllUserAccountByUserId(int userId, bool trackChanges)
        {
            var user = await _repositoryManagerUser.UserRepository.GetEntityById(userId,trackChanges);
            if (user == null)
                throw new EntityNotFoundException(userId, "user");

            var userAccounts = await _repositoryPaymentManager.UserAccountRepository.GetAllUserAccountByUserId(userId, trackChanges);
            if (userAccounts == null)
                throw new EntityNotFoundException(userId, "UserAccounts");
            return userAccounts.Adapt<IEnumerable<UserAccountDto>>();

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

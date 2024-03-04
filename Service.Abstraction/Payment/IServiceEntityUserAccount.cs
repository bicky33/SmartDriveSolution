using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Domain.Enum;

namespace Service.Abstraction.Payment
{
    public interface IServiceEntityUserAccount
    {
        Task<IEnumerable<UserAccountDto>> GetAllAsync(bool trackChanges);
        Task<UserAccountDto> GetByIdAsync(int id, bool trackChanges);
        Task<UserAccountDto> GetByAccountNoAsync(string id, bool trackChanges, ReturnException returnException);
        Task<IEnumerable<UserAccountDto>> GetAllUserAccountByUserId(int userId, bool trackChanges);
        Task<UserAccountDto> CreateAsync(AccountTypeEnum accountType, UserAccountCreateDto entity);
        Task UpdateAsync(int id, UserAccountDto entity);
        Task DeleteAsync(int id);
    }

    public enum ReturnException
    {
        RETURN_WHEN_EXIST,
        RETURN_WHEN_NULL
    }
}

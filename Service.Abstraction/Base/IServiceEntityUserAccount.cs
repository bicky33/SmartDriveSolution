using Contract.DTO.Payment;
using Domain.Entities.Payment;

namespace Service.Abstraction.Base
{
    public interface IServiceEntityUserAccount
    {
        Task<IEnumerable<UserAccountDto>> GetAllAsync(bool trackChanges);
        Task<UserAccountDto> GetByIdAsync(string id, bool trackChanges);
        Task CreateAsync(UserAccountDto entity);
        Task UpdateAsync(string id, UserAccountDto entity);
        Task DeleteAsync(string id);
    }
}

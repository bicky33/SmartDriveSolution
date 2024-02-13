using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Service.Abstraction.Base;

namespace Service.Payment
{
    public class UserAccountService : IServiceEntityBase<UserAccountDto>
    {
        public Task<UserAccountDto> CreateAsync(UserAccountDto entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserAccountDto>> GetAllAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<UserAccountDto> GetByIdAsync(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<UserAccountDto> UpdateAsync(int id, UserAccountDto entity)
        {
            throw new NotImplementedException();
        }
    }
}

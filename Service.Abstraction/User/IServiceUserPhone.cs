using Contract.DTO.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.User
{
    public interface IServiceUserPhone
    {
        Task<IEnumerable<UserPhoneDto>> GetAllAsync(bool trackChanges);
        Task<IEnumerable<UserPhoneDto>> GetAllByIdAsync(int id, bool trackChanges);
        Task<UserPhoneDto> GetByIdAndPhoneNumberAsync(int id, string phoneNumber, bool trackChanges);
        Task<UserPhoneDto> CreateAsync(UserPhoneDto entity);
        Task DeleteAsync(int id, string phoneNumber);
        Task<UserPhoneDto> UpdateAsync(int id, string phoneNumber, UserPhoneDto entity);
    }
}

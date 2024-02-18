using Contract.DTO.UserModule;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.User
{
    public interface IServiceUserRole
    {
        Task<IEnumerable<UserRoleDto>> GetAllAsync(bool trackChanges);
        Task<IEnumerable<UserRoleDto>> GetAllByIdAsync(int id, bool trackChanges);
        Task<UserRoleDto> GetByIdAsync(int id, bool trackChanges);
        Task<UserRoleDto> CreateAsync(UserRoleDto entity);
        Task DeleteAsync(int id, string roleName);
    }
}

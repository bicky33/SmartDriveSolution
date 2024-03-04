using Contract.DTO.UserModule;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.User
{
    public interface IServiceRole
    {
        Task<IEnumerable<RoleDto>> GetAllAsync(bool trackChanges);
        Task<RoleDto> GetByRoleNameAsync(string roleName, bool trackChanges);
        Task<RoleDto> CreateAsync(RoleDto entity);
        Task UpdateAsync(string roleName, RoleDto entity);
        Task DeleteAsync(string roleName);
    }
}

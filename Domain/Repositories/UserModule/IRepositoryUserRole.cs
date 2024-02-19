using Domain.Entities.Users;
using Domain.Enum;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.UserModule
{
    public interface IRepositoryUserRole : IRepositoryEntityBase<UserRole>
    {
        Task<IEnumerable<UserRole>> GetAllEntityById(int id, bool trackChanges);
        Task<UserRole> GetSingleUserRoleByIdAndUserRole(int id, string roleName, bool trackChanges);
    }
}

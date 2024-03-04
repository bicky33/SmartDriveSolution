using Domain.Entities.Users;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.UserModule
{
    public interface IRepositoryRole
    {
        Task<IEnumerable<Role>> GetAllEntity(bool trackChanges);

        void CreateEntity(Role entity);

        void DeleteEntity(Role entity);
        Task<Role> GetRole(string roleName, bool trackChanges);
    }
}

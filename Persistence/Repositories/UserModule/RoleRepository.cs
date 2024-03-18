using Domain.Entities.Users;
using Domain.Repositories.UserModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.UserModule
{
    public class RoleRepository : RepositoryBase<Role>, IRepositoryRole
    {
        public RoleRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Role entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Role entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Role>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(v => v.RoleName).ToListAsync();
        }

        public async Task<Role> GetRole(string roleName, bool trackChanges)
        {
            return await GetByCondition(v => v.RoleName == roleName, trackChanges).SingleOrDefaultAsync();
        }

    }
}

using Domain.Entities.Users;
using Domain.Enum;
using Domain.Repositories.Base;
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
    public class UserRoleRepository : RepositoryBase<UserRole>, IRepositoryUserRole
    {
        public UserRoleRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(UserRole entity)
        {
            Create(entity);
        }

        public void DeleteEntity(UserRole entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<UserRole>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(v => v.UsroEntityid).ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetAllEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(v => v.UsroEntityid == id, trackChanges).ToListAsync();
        }

        public async Task<UserRole> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(v => v.UsroEntityid == id, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<UserRole> GetSingleUserRoleByIdAndUserRole(int id, string roleName, bool trackChanges)
        {
            return await GetByCondition(v => v.UsroEntityid == id && v.UsroRoleName == roleName, trackChanges).SingleOrDefaultAsync();
        }
    }
}

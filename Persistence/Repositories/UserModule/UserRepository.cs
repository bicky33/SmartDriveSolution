using Domain.Entities.Users;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.UserModule
{
    public class UserRepository : RepositoryBase<User>, IRepositoryEntityBase<User>
    {
        public UserRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(User entity)
        {
            Create(entity);
        }

        public void DeleteEntity(User entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<User>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(v => v.UserEntityid).ToListAsync();
        }

        public async Task<User> GetEntityById(int? id, bool trackChanges)
        {
            return await GetByCondition(v => v.UserEntityid == id, trackChanges).SingleOrDefaultAsync();
        }
    }
}

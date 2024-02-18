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
    public class UserAddressRepository : RepositoryBase<UserAddress>, IRepositoryUserAddress
    {
        public UserAddressRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(UserAddress entity)
        {
            Create(entity);
        }

        public void DeleteEntity(UserAddress entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<UserAddress>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(v => v.UsdrId).ToListAsync();
        }

        public async Task<IEnumerable<UserAddress>> GetAllEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(v => v.UsdrEntityid == id, trackChanges).ToListAsync();
        }

        public async Task<UserAddress> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(v => v.UsdrId == id, trackChanges).SingleOrDefaultAsync();
        }
    }
}

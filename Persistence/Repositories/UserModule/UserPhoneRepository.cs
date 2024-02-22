using Domain.Entities.Users;
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
    public class UserPhoneRepository : RepositoryBase<UserPhone>, IRepositoryUserPhone
    {
        public UserPhoneRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(UserPhone entity)
        {
            Create(entity);
        }

        public void DeleteEntity(UserPhone entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<UserPhone>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(v => v.UsphEntityid).ToListAsync();
        }

        public async Task<IEnumerable<UserPhone>> GetAllEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(v => v.UsphEntityid == id, trackChanges).ToListAsync();
        }

        public async Task<UserPhone> GetByPhoneNumber(string phoneNumber, bool trackChanges)
        {
            return await GetByCondition(v => v.UsphPhoneNumber == phoneNumber, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<UserPhone> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(v => v.UsphEntityid == id, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<UserPhone> GetUserPhoneByIdAndPhone(int id, string userPhone, bool trackChanges)
        {
            return await GetByCondition(v => v.UsphEntityid == id && v.UsphPhoneNumber == userPhone, trackChanges).SingleOrDefaultAsync();
        }
    }
}

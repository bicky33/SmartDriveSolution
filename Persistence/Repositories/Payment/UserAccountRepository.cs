﻿using Domain.Entities.Payment;
using Domain.Repositories.Base;
using Domain.Repositories.Payment;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Payment
{
    public class UserAccountRepository : RepositoryBase<UserAccount>, IRepositoryEntityUserAccount
    {
        public UserAccountRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(UserAccount entity)
        {
            Create(entity);
        }

        public void DeleteEntity(UserAccount entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<UserAccount>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.UsacId).ToListAsync();
        }

        public async Task<UserAccount> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(x => x.UsacId.Equals(id), trackChanges).SingleOrDefaultAsync();

        }

        public async Task<UserAccount> GetUserAccountByAccountNo(string id, bool trackChanges)
        {
            return await GetByCondition(x => x.UsacAccountno.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

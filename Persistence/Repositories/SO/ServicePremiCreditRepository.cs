﻿using Domain.Entities.SO;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.SO
{
    public class ServicePremiCreditRepository : RepositoryBase<ServicePremiCredit>, IRepositorySOEntityBase<ServicePremiCredit, int>
    {
        public ServicePremiCreditRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(ServicePremiCredit entity)
        {
            Create(entity);
        }

        public void DeleteEntity(ServicePremiCredit entity)
        {
            Delete(entity);

        }

        public async Task<IEnumerable<ServicePremiCredit>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.SecrId).ToListAsync();
        }

        public async Task<ServicePremiCredit> GetEntityById(int id, bool trackChanges)
        {
            var newId = (int)id;
            return await GetByCondition(c => c.SecrId.Equals(newId), trackChanges).SingleOrDefaultAsync();
        }

    }
}

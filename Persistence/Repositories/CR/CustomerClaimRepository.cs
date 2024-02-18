using Domain.Entities.CR;
using Domain.Repositories.Base;
using Domain.Repositories.CR;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CR
{
    public class CustomerClaimRepository : RepositoryBase<CustomerClaim>, ICustomerClaimRepository
    {
        public CustomerClaimRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public CustomerClaim CreateData(CustomerClaim entity)
        {
            CreateEntity(entity);
            return entity;
        }

        public void CreateEntity(CustomerClaim entity)
        {
            Create(entity);
        }

        public void DeleteEntity(CustomerClaim entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<CustomerClaim>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.CuclCreqEntityid).ToListAsync();
        }

        public async Task<CustomerClaim> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(x => x.CuclCreqEntityid.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

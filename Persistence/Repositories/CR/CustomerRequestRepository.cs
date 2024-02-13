using Domain.Entities.CR;
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
    public class CustomerRequestRepository : RepositoryBase<CustomerRequest>, ICustomerRequestRepository
    {
        public CustomerRequestRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(CustomerRequest entity)
        {
            Create(entity);
        }

        public void DeleteEntity(CustomerRequest entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<CustomerRequest>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.CreqEntityid).ToListAsync();
        }

        public async Task<CustomerRequest> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(x => x.CreqEntityid.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

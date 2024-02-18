using Domain.Entities.CR;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CR
{
    public class CustomerInscExtendRepository : RepositoryBase<CustomerInscExtend>, IRepositoryEntityBase<CustomerInscExtend>
    {
        public CustomerInscExtendRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(CustomerInscExtend entity)
        {
            Create(entity);
        }

        public void DeleteEntity(CustomerInscExtend entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<CustomerInscExtend>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.CuexId).ToListAsync();
        }

        public async Task<CustomerInscExtend> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(x => x.CuexId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

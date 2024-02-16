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
    public class CustomerInscDocRepository : RepositoryBase<CustomerInscDoc>, IRepositoryEntityBase<CustomerInscDoc>
    {
        public CustomerInscDocRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(CustomerInscDoc entity)
        {
            Create(entity);
        }

        public void DeleteEntity(CustomerInscDoc entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<CustomerInscDoc>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.CadocId).ToListAsync();
        }

        public async Task<CustomerInscDoc> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(x => x.CadocId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

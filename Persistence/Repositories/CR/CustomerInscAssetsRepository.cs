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
    public class CustomerInscAssetsRepository : RepositoryBase<CustomerInscAsset>, IRepositoryEntityBase<CustomerInscAsset>
    {
        public CustomerInscAssetsRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(CustomerInscAsset entity)
        {
            Create(entity);
        }

        public void DeleteEntity(CustomerInscAsset entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<CustomerInscAsset>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.CiasCreqEntityid).ToListAsync();
        }

        public async Task<CustomerInscAsset> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(x => x.CiasCreqEntityid.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

using Contract.DTO.SO;
using Domain.Entities.SO;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.SO
{
    public class ClaimAssetEvidenceRepository : RepositoryBase<ClaimAssetEvidence>, IRepositorySOEntityBase<ClaimAssetEvidence,int>
    {
        public ClaimAssetEvidenceRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(ClaimAssetEvidence entity)
        {
            Create(entity);
        }

        public void DeleteEntity(ClaimAssetEvidence entity)
        {
            Delete(entity);

        }

        public async Task<IEnumerable<ClaimAssetEvidence>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x=>x.CaevId).ToListAsync();
        }

        public async Task<ClaimAssetEvidence> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.CaevId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
        
    }
}

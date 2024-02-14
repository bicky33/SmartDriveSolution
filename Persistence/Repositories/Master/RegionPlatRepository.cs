using Domain.Entities.Master;
using Domain.Repositories.Base;
using Domain.Repositories.Base.Master;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class RegionPlatRepository : RepositoryBase<RegionPlat>, IRepositoryEntityBaseMaster<RegionPlat>
    {
        public RegionPlatRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntityMaster(RegionPlat entity)
        {
            Create(entity);
        }

        public void DeleteEntityMaster(RegionPlat entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<RegionPlat>> GetAllEntityMaster(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(rp => rp.RegpName).ToListAsync();
        }

        public async Task<RegionPlat> GetEntityByNameMaster(string name, bool trackChanges)
        {
            return await GetByCondition(rp => rp.RegpName.Equals(name), trackChanges).SingleOrDefaultAsync();
        }
    }
}
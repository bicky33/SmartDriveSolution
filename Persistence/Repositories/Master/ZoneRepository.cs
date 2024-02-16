using Domain.Entities.Master;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class ZoneRepository : RepositoryBase<Zone>, IRepositoryEntityBase<Zone>
    {
        public ZoneRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Zone entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Zone entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Zone>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(z =>z.ZonesId).ToListAsync();
        }

        public async Task<Zone> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(z => z.ZonesId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}
using Domain.Entities.Master;
using Domain.Repositories.Base;
using Domain.Repositories.Base.Master;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class AreaWorkgroupRepository : RepositoryBase<AreaWorkgroup>, IRepositoryEntityBaseMaster<AreaWorkgroup>
    {
        public AreaWorkgroupRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntityMaster(AreaWorkgroup entity)
        {
            Create(entity);
        }

        public void DeleteEntityMaster(AreaWorkgroup entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<AreaWorkgroup>> GetAllEntityMaster(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(aw => aw.ArwgCode).ToListAsync();
        }

        public async Task<AreaWorkgroup> GetEntityByNameMaster(string name, bool trackChanges)
        {
            return await GetByCondition(aw => aw.ArwgCode.Equals(name), trackChanges).SingleOrDefaultAsync();
        }
    }
}
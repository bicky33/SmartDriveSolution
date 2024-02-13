using Domain.Entities.Partners;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;


namespace Persistence.Repositories.Partners
{
    public class RepositoryPartner : RepositoryBase<Partner>, IRepositoryEntityBase<Partner>
    {
        public RepositoryPartner(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Partner entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Partner entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Partner>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(c => c.PartEntityid).ToListAsync();
        }

        public async Task<Partner?> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.PartEntityid.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

using Domain.Entities.SO;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.SO
{
    public class ServicePremiRepository : RepositoryBase<ServicePremi>, IRepositorySOEntityBase<ServicePremi, int>
    {
        public ServicePremiRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(ServicePremi entity)
        {
            Create(entity);
        }

        public void DeleteEntity(ServicePremi entity)
        {
            Delete(entity);

        }

        public async Task<IEnumerable<ServicePremi>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.SemiServId).ToListAsync();
        }

        public async Task<ServicePremi> GetEntityById(int id, bool trackChanges)
        {
            var newId = (int)id;
            return await GetByCondition(c => c.SemiServId.Equals(newId), trackChanges).SingleOrDefaultAsync();
        }

    }
}

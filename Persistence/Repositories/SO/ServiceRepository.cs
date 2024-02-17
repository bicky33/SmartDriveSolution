using Domain.Entities.SO;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;


namespace Persistence.Repositories.SO
{
    public class ServiceRepository : RepositoryBase<Service>, IRepositorySOEntityBase<Service,int>
    {
        public ServiceRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Service entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Service entity)
        {
            Delete(entity);

        }

        public async Task<IEnumerable<Service>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x=>x.ServId).ToListAsync();
        }

        public async Task<Service> GetEntityById(int id, bool trackChanges)
        {
            var newId = (int)id;
            return await GetByCondition(c => c.ServId.Equals(newId), trackChanges).SingleOrDefaultAsync();
        }
        
    }
}

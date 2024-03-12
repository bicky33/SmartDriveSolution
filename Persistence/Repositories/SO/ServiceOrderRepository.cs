using Domain.Entities.SO;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.SO
{
    public class ServiceOrderRepository : RepositoryBase<ServiceOrder>, IRepositorySOEntityBase<ServiceOrder, string>
    {
        public ServiceOrderRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(ServiceOrder entity)
        {
            Create(entity);
        }

        public void DeleteEntity(ServiceOrder entity)
        {
            Delete(entity);

        }

        public async Task<IEnumerable<ServiceOrder>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).Include(c => c.SeroServ).OrderBy(x => x.SeroId).ToListAsync();
        }

        public async Task<ServiceOrder> GetEntityById(string id, bool trackChanges)
        {
            return await GetByCondition(c => c.SeroId.Equals(id), trackChanges)
                .Include(c => c.ServiceOrderTasks)
                    .ThenInclude(c => c.ServiceOrderWorkorders)
                .SingleOrDefaultAsync();
        }

    }
}

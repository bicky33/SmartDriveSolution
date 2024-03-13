using Domain.Entities.SO;
using Domain.RequestFeatured;
using Domain.RequestFeatured.SO;
using Microsoft.EntityFrameworkCore;
using static Domain.Repositories.SO.IRepositoryWithPagingSO;

namespace Persistence.Repositories.SO
{
    public class ServiceOrderRepository : RepositoryBase<ServiceOrder>, IRepositoryWithPagingSO<ServiceOrder, string>
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

        public async Task<IEnumerable<ServiceOrder>> GetAllByAgentId(int id, bool trackChanges)
        {
            return await GetByCondition(s => s.SeroAgentEntityid == id,trackChanges).ToListAsync();
        }

        public async Task<ServiceOrder> GetEntityById(string id, bool trackChanges)
        {
            return await GetByCondition(c => c.SeroId.Equals(id), trackChanges)
                .Include(c => c.ServiceOrderTasks)
                    .ThenInclude(c => c.ServiceOrderWorkorders)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ServiceOrder>> GetWithPaging(EntityParameterSO entityParams, bool trackChanges)
        {
            //with searchby
            IQueryable<ServiceOrder> serviceOrders=GetAll(trackChanges).Include(c=>c.SeroServ).OrderBy(x => x.SeroId);


            if (entityParams.SearchType != null) serviceOrders=serviceOrders.Where(s => s.SeroServ!.ServType!.Equals(entityParams.SearchType));
            if (entityParams.SearchStatus != null) serviceOrders = serviceOrders.Where(s => s.SeroServ!.ServStatus!.Equals(entityParams.SearchStatus));
            if (entityParams.SearchStartDate != null)
            {
                var strDate = entityParams.SearchStartDate!.Split("-");
                var startDate = DateTime.Parse($"{strDate[1]}/{strDate[2]}/{strDate[0]}");
                serviceOrders = serviceOrders.Where(s => s.SeroServ!.ServCreatedOn >= startDate);
            }
            if (entityParams.SearchEndDate != null)
            {
                var strDate = entityParams.SearchEndDate!.Split("-");
                var endDate = DateTime.Parse($"{strDate[1]}/{strDate[2]}/{strDate[0]}");
                serviceOrders = serviceOrders.Where(s => s.SeroServ!.ServCreatedOn <= endDate);
            }

            return await serviceOrders.ToListAsync();
        }
    }
}

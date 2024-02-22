using Domain.Entities.SO;
using Domain.Entities.Users;
using Domain.Exceptions.SO;
using Domain.Repositories.SO;
using Microsoft.AspNetCore.Routing.Tree;
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
            return await GetByCondition(c => c.ServId.Equals(newId), trackChanges)
                    .Include(c => c.ServCustEntity)
                    .Include(c => c.ServCreqEntity)
                    .Include(c => c.ServiceOrders)
                        .ThenInclude(c=>c.ServiceOrderTasks)
                            .ThenInclude(c=>c.ServiceOrderWorkorders)
                    .Include(c => c.ServicePremi)
                    .Include(c => c.ServicePremiCredits)
                    .Select(c => new Service
                    {
                        ServId = c.ServId,
                        ServCreatedOn = c.ServCreatedOn,
                        ServStatus = c.ServStatus,
                        ServStartdate = c.ServStartdate,
                        ServEnddate = c.ServEnddate,
                        ServCreqEntityid = c.ServCreqEntityid,
                        ServCustEntityid = c.ServCustEntityid,
                        ServType = c.ServType,
                        ServicePremi = c.ServicePremi,
                        ServicePremiCredits = c.ServicePremiCredits,
                        ServVehicleNo = c.ServVehicleNo,
                        ServInsuranceNo = c.ServInsuranceNo,
                        ServServId = c.ServServId,
                        ServCustEntity = new User
                        {
                            UserFullName = c.ServCustEntity.UserFullName,
                        },
                        ServiceOrders = c.ServiceOrders,
                    })
                    .FirstOrDefaultAsync();
        }
        
    }
}

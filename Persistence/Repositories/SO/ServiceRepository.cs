using Domain.Entities.SO;
using Domain.Entities.Users;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories.SO
{
    public class ServiceRepository : RepositoryBase<Domain.Entities.SO.Service>, IRepositorySOEntityBase<Domain.Entities.SO.Service, int>
    {
        public ServiceRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Domain.Entities.SO.Service entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Domain.Entities.SO.Service entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Domain.Entities.SO.Service>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.ServId).ToListAsync();
        }

        public async Task<Domain.Entities.SO.Service> GetEntityById(int id, bool trackChanges)
        {
            var newId = (int)id;
            return await GetByCondition(c => c.ServId.Equals(newId), trackChanges)
                    .Include(c => c.ServCustEntity)
                    .Include(c => c.ServCreqEntity)
                        .ThenInclude(c => c.CreqAgenEntity)
                            .ThenInclude(c => c.EawgEntity)
                    .Include(c => c.ServCreqEntity)
                        .ThenInclude(c => c.CustomerInscAsset)
                    .Include(c => c.ServiceOrders)
                        .ThenInclude(c => c.ServiceOrderTasks)
                            .ThenInclude(c => c.ServiceOrderWorkorders)
                    .Include(c => c.ServicePremi)
                    .Include(c => c.ServicePremiCredits)
                    .Select(c => new Domain.Entities.SO.Service
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
                        ServInsuranceNo = c.ServInsuranceNo,
                        ServVehicleNo=c.ServVehicleNo,
                        ServServId = c.ServServId,
                        ServCustEntity = new User
                        {
                            UserFullName = c.ServCustEntity!.UserFullName,
                        },
                        ServCreqEntity = new Domain.Entities.CR.CustomerRequest
                        {
                            CreqAgenEntity = new Domain.Entities.HR.EmployeeAreWorkgroup
                            {
                                EawgEntity = new Domain.Entities.HR.Employee
                                {
                                    EmpName = c.ServCreqEntity!.CreqAgenEntity!.EawgEntity.EmpName
                                }
                            },
                            CustomerInscAsset = c.ServCreqEntity.CustomerInscAsset,
                        },
                        ServiceOrders = c.ServiceOrders,

                    })
                    .FirstOrDefaultAsync();
        }

    }
}

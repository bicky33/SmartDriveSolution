using Domain.Entities.HR;
using Domain.Entities.Users;
using Domain.Repositories.HR;
using Domain.RequestFeatured;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.HR
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

      /*  public void CreateEmployee(BusinessEntity be, User user, IEnumerable<UserAddress> userAddress, IEnumerable<UserPhone> userPhone, IEnumerable<UserRole> userRole, Employee e)
        {
*//*            _dbContext.BusinessEntities.Add(be);
            _dbContext.UserPhones.Add(userPhone);
            _dbContext.UserRoles.Add(userRole);
            _dbContext.UserAddresses.Add(userAddress);
            _dbContext.Employees.Add(e);*//*
        }*/

        public void CreateEntity(Employee entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Employee entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Employee>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(c => c.EmpEntityid).ToListAsync();
        }

        public async Task<PagedList<Employee>> GetAllPaging(EntityParameter entityParams, bool trackChanges)
        {
            var categories = GetByCondition(C => C.EmpName.StartsWith(entityParams.SearchBy), false).OrderBy(c => c.EmpEntityid);
            return PagedList<Employee>.ToPagedList(categories, entityParams.PageNumber, entityParams.PageSize);
        }

        public async Task<Employee> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.EmpEntityid.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

using Domain.Entities.HR;
using Domain.Entities.Partners;
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

        public void CreateEmployee(BusinessEntity be, User user, UserAddress userAddress, UserPhone userPhone, UserRole userRole, Employee e)
        {
            _dbContext.BusinessEntities.Add(be);
            _dbContext.Users.Add(user);
            _dbContext.UserPhones.Add(userPhone);
            _dbContext.UserRoles.Add(userRole);
            _dbContext.UserAddresses.Add(userAddress);
            _dbContext.Employees.Add(e);
        }

        public void CreateEntity(Employee entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Employee entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Employee>> FindEmployeeById(int id)
        {

            return await _dbContext.Employees.Where(x => x.SoftDelete == "ACTIVE" && x.EmpEntityid == id).Include(x => x.EmpJobCodeNavigation).Select(x => new Employee
            {
                EmpName = x.EmpName,
                EmpJoinDate = x.EmpJoinDate,
                EmpGraduate = x.EmpGraduate,
                EmpJobCodeNavigation = new JobType
                {
                    JobDesc = x.EmpJobCodeNavigation.JobDesc,
                },
                EmpNetSalary = x.EmpNetSalary,
                EmpAccountNumber = x.EmpAccountNumber,
                EmpEntityid = x.EmpEntityid,
            }).OrderBy(x => x.EmpName).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEntity(bool trackChanges)
        {
            return await _dbContext.Employees.Where(x => x.SoftDelete == "ACTIVE").Include(x => x.EmpJobCodeNavigation).Select(x => new Employee
            {
                EmpName = x.EmpName, 
                EmpJoinDate = x.EmpJoinDate,
                EmpGraduate = x.EmpGraduate,
                EmpJobCodeNavigation = new JobType
                {
                    JobDesc = x.EmpJobCodeNavigation.JobDesc,
                },
                EmpNetSalary = x.EmpNetSalary,
                EmpAccountNumber = x.EmpAccountNumber,
                EmpEntityid = x.EmpEntityid,
            }).OrderBy(x => x.EmpName).ToListAsync();
        }


        public async Task<PagedList<Employee>> GetAllPaging(EntityParameter entityParams, bool trackChanges)
        {
            //users = users.Where(u => EF.Functions.Like(u.UserName, $"%{entityParams.SearchBy}%"));
            IQueryable <Employee>categories =  _dbContext.Employees.Where(x => x.SoftDelete == "ACTIVE"  && EF.Functions.Like(x.EmpName, $"%{entityParams.SearchBy}%")).Include(x => x.EmpJobCodeNavigation).Select(x => new Employee
            {
                EmpName = x.EmpName,
                EmpJoinDate = x.EmpJoinDate,
                EmpGraduate = x.EmpGraduate,
                EmpJobCodeNavigation = new JobType
                {
                    JobDesc = x.EmpJobCodeNavigation.JobDesc,
                },
                EmpNetSalary = x.EmpNetSalary,
                EmpAccountNumber = x.EmpAccountNumber,
                EmpEntityid = x.EmpEntityid,
            }).OrderBy(x => x.EmpName);
            return PagedList<Employee>.ToPagedList(categories, entityParams.PageNumber, entityParams.PageSize);
        }



        public async Task<Employee> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.EmpEntityid.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

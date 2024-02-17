using Domain.Entities.HR;
using Domain.Entities.Master;
using Domain.Repositories.HR;
using Domain.Repositories.HR.RequestFeature;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.HR
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

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

using Domain.Entities.HR;
using Domain.Repositories.HR;
using Domain.RequestFeatured;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.HR
{
    public class EmployeeSalaryDetailRepository : RepositoryBase<EmployeeSalaryDetail>, IEmployeeSalaryDetailRepository
    {
        public EmployeeSalaryDetailRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(EmployeeSalaryDetail entity)
        {
            Create(entity);
        }

        public void DeleteEntity(EmployeeSalaryDetail entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<EmployeeSalaryDetail>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(c => c.EmsaId).ToListAsync();
        }

        public async Task<PagedList<EmployeeSalaryDetail>> GetAllPaging(EntityParameter entityParams, bool trackChanges)
        {
            var emsa = GetByCondition(C => C.EmsaName.StartsWith(entityParams.SearchBy), false).OrderBy(c => c.EmsaId);
            return PagedList<EmployeeSalaryDetail>.ToPagedList(emsa, entityParams.PageNumber, entityParams.PageSize);
        }

        public async Task<EmployeeSalaryDetail> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.EmsaId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

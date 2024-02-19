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
    public class EmployeeArwgRepository : RepositoryBase<EmployeeAreWorkgroup>, IEmployeeArwgRepository
    {
        public EmployeeArwgRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(EmployeeAreWorkgroup entity)
        {
            Create(entity);
        }

        public void DeleteEntity(EmployeeAreWorkgroup entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<EmployeeAreWorkgroup>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(c => c.EawgId).ToListAsync();
        }

        public async Task<EmployeeAreWorkgroup> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.EawgId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<PagedList<EmployeeAreWorkgroup>> GetAllPaging(EntityParameter entityParams, bool trackChanges)
        {
            var arwg = GetByCondition(C => C.EawgArwgCode.StartsWith(entityParams.SearchBy), false).OrderBy(c => c.EawgId);
            return  PagedList<EmployeeAreWorkgroup>.ToPagedList(arwg, entityParams.PageNumber, entityParams.PageSize);
        }
    }
}

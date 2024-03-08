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
    public class TemplateSalaryRepository : RepositoryBase<TemplateSalary>, ITemplateSalaryRepository
    {
        public TemplateSalaryRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(TemplateSalary entity)
        {
            Create(entity);
        }

        public void DeleteEntity(TemplateSalary entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<TemplateSalary>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(c => c.TesalId).ToListAsync();
        }

        public async Task<PagedList<TemplateSalary>> GetAllPaging(EntityParameter entityParams, bool trackChanges)
        {

            var besa = GetByCondition(C => C.TesalName.StartsWith(entityParams.SearchBy), false).OrderBy(c => c.TesalId);
            return PagedList<TemplateSalary>.ToPagedList(besa, entityParams.PageNumber, entityParams.PageSize);
        }

        public async Task<TemplateSalary> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.TesalId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

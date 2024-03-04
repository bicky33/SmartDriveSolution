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
    public class BatchEmployeeSalaryRepository : RepositoryBase<BatchEmployeeSalary>, IBatchEmployeeSalaryRepository
    {
        public BatchEmployeeSalaryRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(BatchEmployeeSalary entity)
        {
            Create(entity);
        }

        public void DeleteEntity(BatchEmployeeSalary entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<BatchEmployeeSalary>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(c => c.BesaEmpEntityId).ToListAsync();
        }

        public async Task<PagedList<BatchEmployeeSalary>> GetAllPaging(EntityParameter entityParams, bool trackChanges)
        {
            var besa = GetByCondition(C => C.BesaAccountNumber.StartsWith(entityParams.SearchBy), false).OrderBy(c => c.BesaEmpEntityId);
            return PagedList<BatchEmployeeSalary>.ToPagedList(besa, entityParams.PageNumber, entityParams.PageSize);
        }

        public async Task<BatchEmployeeSalary> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.BesaEmpEntityId.Equals(id), trackChanges).SingleOrDefaultAsync();
        } 
    }
}

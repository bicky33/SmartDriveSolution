using Domain.Entities.HR;
using Domain.Repositories.Base;
using Domain.Repositories.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Persistence.Base;
using Service.Abstraction.Base;
using Services.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.HR
{
    public class JobTypeRepository : RepositoryBase<JobType>, IJobTypeRepository
    {
        public JobTypeRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(JobType entity)
        {
            Create(entity);
        }

        public void DeleteEntity(JobType entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<JobType>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(c => c.JobCode).ToListAsync();
        }

        public async Task<JobType> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.JobCode.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<JobType> GetJobTypeById(string id, bool trackChanges)
        {
            return await GetByCondition(c => c.JobCode.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}

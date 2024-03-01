using Domain.Entities.SO;
using Domain.Exceptions.SO;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.SO
{
    public class ServiceOrderTaskRepository : RepositoryBase<ServiceOrderTask>, IRepositorySOEntityBase<ServiceOrderTask,int>
    {
        public ServiceOrderTaskRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(ServiceOrderTask entity)
        {
            Create(entity);
        }

        public void DeleteEntity(ServiceOrderTask entity)
        {
            Delete(entity);

        }
        public async Task<IEnumerable<ServiceOrderTask>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x=>x.SeotId).ToListAsync();
        }

        public async Task<ServiceOrderTask> GetEntityById(int id, bool trackChanges)
        {
            var newId = (int)id;
            return await GetByCondition(c => c.SeotId.Equals(newId), trackChanges)
                .Include(c=>c.ServiceOrderWorkorders)
                .SingleOrDefaultAsync();
        }
        
    }
}

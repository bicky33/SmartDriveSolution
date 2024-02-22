using Domain.Entities.SO;
using Domain.Entities.Users;
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
    public class ServiceOrderRepository : RepositoryBase<ServiceOrder>, IRepositorySOEntityBase<ServiceOrder,string>
    {
        public ServiceOrderRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(ServiceOrder entity)
        {
            Create(entity);
        }

        public void DeleteEntity(ServiceOrder entity)
        {
            Delete(entity);

        }

        public async Task<IEnumerable<ServiceOrder>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x=>x.SeroId).ToListAsync();
        }

        public async Task<ServiceOrder> GetEntityById(string id, bool trackChanges)
        {
            return await GetByCondition(c => c.SeroId.Equals(id), trackChanges)
                .Include(c=>c.ServiceOrderTasks)
                    .ThenInclude(c=>c.ServiceOrderWorkorders)
                .SingleOrDefaultAsync();
        }
        
    }
}

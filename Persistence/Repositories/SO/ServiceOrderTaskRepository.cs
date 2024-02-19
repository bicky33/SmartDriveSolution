using Contract.DTO.SO;
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
    public class ServiceOrderTaskRepository : RepositoryBase<ServiceOrderTask>, IRepositoryRelationSOBase<ServiceOrderTask,int>
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

        public async Task<IEnumerable<ServiceOrderTask>> GetAllByRelation(string name, object value, bool trackChanges)
        {
            if (name.Equals("SeroId"))
                return await GetByCondition(c => c.SeotSeroId.Equals(value),trackChanges).Include(seot=>seot.ServiceOrderWorkorders).ToListAsync();
            throw new RelationNotFoundExceptionSO(name, "Service Order Task");
        }

        public async Task<IEnumerable<ServiceOrderTask>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x=>x.SeotId).ToListAsync();
        }

        public async Task<ServiceOrderTask> GetEntityById(int id, bool trackChanges)
        {
            var newId = (int)id;
            return await GetByCondition(c => c.SeotId.Equals(newId), trackChanges).SingleOrDefaultAsync();
        }
        
    }
}

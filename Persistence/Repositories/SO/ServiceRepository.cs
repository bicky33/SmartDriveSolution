using Contract.DTO.SO;
using Domain.Entities.SO;
using Domain.Exceptions.SO;
using Domain.Repositories.SO;
using Microsoft.AspNetCore.Routing.Tree;
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
    public class ServiceRepository : RepositoryBase<Service>, IRepositoryRelationSOBase<Service,int>
    {
        public ServiceRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Service entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Service entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Service>> GetAllByRelation(string name, object value,bool trackChanges)
        {
            if (name.Equals("SeroId"))
                return await GetByCondition(c => c.ServiceOrders.Any(so => so.SeroId.Equals(value)), trackChanges).ToListAsync();
            if (name.Equals("feasbility"))
                return await GetAll(trackChanges)
                                .Include(c => c.ServCustEntity)
                                .Include(c => c.ServCreqEntity)
                                    .ThenInclude(c=>c.CreqAgenEntity)
                                        .ThenInclude(c=>c.EawgEntity)
                                        .ToListAsync();
            throw new RelationNotFoundExceptionSO(name, "Service");
        }

        public async Task<IEnumerable<Service>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x=>x.ServId).ToListAsync();
        }

        public async Task<Service> GetEntityById(int id, bool trackChanges)
        {
            var newId = (int)id;
            return await GetByCondition(c => c.ServId.Equals(newId), trackChanges).SingleOrDefaultAsync();
        }
        
    }
}

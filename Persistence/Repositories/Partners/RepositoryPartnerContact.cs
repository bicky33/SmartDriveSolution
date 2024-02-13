using Domain.Entities.Partners;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Partners
{
    public class RepositoryPartnerContact : RepositoryBase<PartnerContact>, IRepositoryEntityBase<PartnerContact>
    {
        public RepositoryPartnerContact(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(PartnerContact entity)
        {
            Create(entity);
        }

        public void DeleteEntity(PartnerContact entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<PartnerContact>> GetAllEntity(bool trackChanges)
        {
           return await GetAll(trackChanges).ToListAsync();
        }

        public Task<PartnerContact?> GetEntityById(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}

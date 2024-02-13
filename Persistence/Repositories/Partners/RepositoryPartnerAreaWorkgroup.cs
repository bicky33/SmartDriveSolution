using Domain.Entities.Partners;
using Domain.Repositories.Base;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Partners
{
    internal class RepositoryPartnerAreaWorkgroup : RepositoryBase<PartnerAreaWorkgroup>, IRepositoryEntityBase<PartnerAreaWorkgroup>
    {
        public RepositoryPartnerAreaWorkgroup(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(PartnerAreaWorkgroup entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(PartnerAreaWorkgroup entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PartnerAreaWorkgroup>> GetAllEntity(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<PartnerAreaWorkgroup?> GetEntityById(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}

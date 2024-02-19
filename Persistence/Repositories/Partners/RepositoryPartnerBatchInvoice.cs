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
    internal class RepositoryPartnerBatchInvoice : RepositoryBase<BatchPartnerInvoice>, IRepositoryEntityBase<BatchPartnerInvoice>
    {
        public RepositoryPartnerBatchInvoice(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(BatchPartnerInvoice entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(BatchPartnerInvoice entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BatchPartnerInvoice>> GetAllEntity(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<BatchPartnerInvoice?> GetEntityById(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}

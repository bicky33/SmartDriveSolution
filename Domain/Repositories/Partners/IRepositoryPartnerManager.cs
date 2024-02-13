using Domain.Entities.Partners;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Partners
{
    public interface IRepositoryPartnerManager
    {
        public IRepositoryEntityBase<Partner> RepositoryPartner { get; }
        public IRepositoryEntityBase<PartnerAreaWorkgroup> RepositoryPartnerAreaWorkgroup { get; }
        public IRepositoryEntityBase<PartnerContact> RepositoryPartnerContact { get; }
        public IRepositoryEntityBase<BatchPartnerInvoice> RepositoryPartnerBatchInvoice { get; }
    }
}

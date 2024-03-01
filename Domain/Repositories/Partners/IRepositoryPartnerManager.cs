using Domain.Entities.Master;
using Domain.Entities.Partners;
using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.Repositories.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Partners
{
    public interface IRepositoryPartnerManager
    {
        public IRepositoryUser RepositoryUser { get; }
        public IRepositoryUserPhone RepositoryUserPhone { get; }
        public IRepositoryUserRole RepositoryUserRole { get; }


        public IRepositoryPartnerWorkOrder RepositoryPartnerWorkOrder { get; }
        public IRepositoryPartner RepositoryPartner { get; }
        public IRepositoryPartnerAreaWorkgroup RepositoryPartnerAreaWorkgroup { get; }
        public IRepositoryPartnerContact RepositoryPartnerContact { get; }
        public IPartnerBatchInvoice RepositoryPartnerBatchInvoice { get; }
        public IRepositoryBusinessEntity<BusinessEntity> RepositoryBusinessEntity { get; }
        public IRepositoryPartnerClaimAssetSparepartBatch RepositoryClaimAssetSparepartBatch { get; }
        public IUnitOfWorks UnitOfWorks { get; }
        public IRepositoryEntityBase<City> RepositoryCity { get; }

    }
}

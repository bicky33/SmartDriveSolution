using Domain.Entities.Master;
using Domain.Entities.Partners;
using Domain.Entities.SO;
using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.Repositories.Partners;
using Domain.Repositories.SO;
using Domain.Repositories.UserModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Master;
using Persistence.Repositories.SO;
using Persistence.Repositories.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Partners
{
    public class RepositoryPartnerManager : IRepositoryPartnerManager
    {
        private readonly Lazy<IRepositoryUserPhone> _repositoryUserPhone;
        private readonly Lazy<IRepositoryUser> _repositoryUser;
        private readonly Lazy<IRepositoryUserRole> _repositoryUserRole;


        private readonly Lazy<IRepositoryPartner> _repositoryPartner;
        private readonly Lazy<IRepositoryPartnerAreaWorkgroup> _repositoryPartnerAreaWorkgroup;
        private readonly Lazy<IRepositoryPartnerContact> _repositoryPartnerContact;
        private readonly Lazy<IPartnerBatchInvoice> _repositoryPartnerBatchInvoice;
        private readonly Lazy<IRepositoryBusinessEntity<BusinessEntity>> _repositoryBusinessEntity;
        private readonly Lazy<IRepositoryPartnerClaimAssetSparepartBatch> _repositoryClaimAssetSparepartBatch;
        private readonly Lazy<IRepositoryPartnerWorkOrder> _repositoryPartnerWorkOrder;
        private readonly Lazy<IRepositoryEntityBase<City>> _repositoryCity;


        private readonly Lazy<IUnitOfWorks> _unitOfWorks;

        public RepositoryPartnerManager(SmartDriveContext _context)
        {
            _repositoryUserPhone = new Lazy<IRepositoryUserPhone>(() => new UserPhoneRepository(_context));
            _repositoryUserRole = new Lazy<IRepositoryUserRole>(() => new UserRoleRepository(_context));
            _repositoryUser = new Lazy<IRepositoryUser>(() => new UserRepository(_context));

            _repositoryPartner = new Lazy<IRepositoryPartner>(() => new RepositoryPartner(_context));
            _repositoryPartnerAreaWorkgroup = new Lazy<IRepositoryPartnerAreaWorkgroup>(() => new RepositoryPartnerAreaWorkgroup(_context));
            _repositoryPartnerContact = new Lazy<IRepositoryPartnerContact>(() => new RepositoryPartnerContact(_context));
            _repositoryPartnerBatchInvoice = new Lazy<IPartnerBatchInvoice>(() => new RepositoryPartnerBatchInvoice(_context));
            _repositoryBusinessEntity = new Lazy<IRepositoryBusinessEntity<BusinessEntity>>(() => new BusinessEntityRepository(_context));
            _repositoryClaimAssetSparepartBatch = new Lazy<IRepositoryPartnerClaimAssetSparepartBatch>(() => new RepositoryPartnerClaimAssetSparepartBatch(_context));
            _repositoryPartnerWorkOrder = new Lazy<IRepositoryPartnerWorkOrder>(() => new RepositoryPartnerWorkOrder(_context));
            _repositoryCity = new Lazy<IRepositoryEntityBase<City>>(() => new CityRepository(_context));


            _unitOfWorks = new Lazy<IUnitOfWorks>(() => new UnitOfWorks(_context));
        }
        public IRepositoryPartner RepositoryPartner => _repositoryPartner.Value;
        public IRepositoryPartnerAreaWorkgroup RepositoryPartnerAreaWorkgroup => _repositoryPartnerAreaWorkgroup.Value;
        public IRepositoryPartnerContact RepositoryPartnerContact => _repositoryPartnerContact.Value;
        public IPartnerBatchInvoice RepositoryPartnerBatchInvoice => _repositoryPartnerBatchInvoice.Value;
        public IRepositoryBusinessEntity<BusinessEntity> RepositoryBusinessEntity => _repositoryBusinessEntity.Value;
        public IUnitOfWorks UnitOfWorks => _unitOfWorks.Value;
        public IRepositoryUser RepositoryUser => _repositoryUser.Value;
        public IRepositoryUserPhone RepositoryUserPhone => _repositoryUserPhone.Value;
        public IRepositoryUserRole RepositoryUserRole => _repositoryUserRole.Value;
        public IRepositoryPartnerClaimAssetSparepartBatch RepositoryClaimAssetSparepartBatch => _repositoryClaimAssetSparepartBatch.Value;
        public IRepositoryPartnerWorkOrder RepositoryPartnerWorkOrder => _repositoryPartnerWorkOrder.Value;

        public IRepositoryEntityBase<City> RepositoryCity => _repositoryCity.Value;

        public IRepositorySOEntityBase<ClaimAssetSparepart, int> RepositoryClaimAssetSparespart => throw new NotImplementedException();
    }
}

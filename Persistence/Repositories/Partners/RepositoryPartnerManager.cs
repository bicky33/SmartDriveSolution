using Domain.Entities.Partners;
using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.Repositories.Partners;
using Domain.Repositories.UserModule;
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
        private readonly Lazy<IRepositoryPartner> _repositoryPartner;
        private readonly Lazy<IRepositoryPartnerAreaWorkgroup> _repositoryPartnerAreaWorkgroup;
        private readonly Lazy<IRepositoryPartnerContact> _repositoryPartnerContact;
        private readonly Lazy<IRepositoryEntityBase<BatchPartnerInvoice>> _repositoryPartnerBatchInvoice;
        private readonly Lazy<IRepositoryBusinessEntity<BusinessEntity>> _repositoryBusinessEntity;
        private readonly Lazy<IUnitOfWorks> _unitOfWorks;

        public RepositoryPartnerManager(SmartDriveContext _context)
        {
            _repositoryPartner = new Lazy<IRepositoryPartner>(() => new RepositoryPartner(_context));
            _repositoryPartnerAreaWorkgroup = new Lazy<IRepositoryPartnerAreaWorkgroup>(() => new RepositoryPartnerAreaWorkgroup(_context));
            _repositoryPartnerContact = new Lazy<IRepositoryPartnerContact>(() => new RepositoryPartnerContact(_context));
            _repositoryPartnerBatchInvoice = new Lazy<IRepositoryEntityBase<BatchPartnerInvoice>>(() => new RepositoryPartnerBatchInvoice(_context));
            _repositoryBusinessEntity = new Lazy<IRepositoryBusinessEntity<BusinessEntity>>(() => new BusinessEntityRepository(_context));
            _unitOfWorks = new Lazy<IUnitOfWorks>(() => new UnitOfWorks(_context));
        }
        public IRepositoryPartner RepositoryPartner => _repositoryPartner.Value;
        public IRepositoryPartnerAreaWorkgroup RepositoryPartnerAreaWorkgroup => _repositoryPartnerAreaWorkgroup.Value;
        public IRepositoryPartnerContact RepositoryPartnerContact => _repositoryPartnerContact.Value;
        public IRepositoryEntityBase<BatchPartnerInvoice> RepositoryPartnerBatchInvoice => _repositoryPartnerBatchInvoice.Value;
        public IRepositoryBusinessEntity<BusinessEntity> RepositoryBusinessEntity => _repositoryBusinessEntity.Value;
        public IUnitOfWorks UnitOfWorks => _unitOfWorks.Value;
    }
}

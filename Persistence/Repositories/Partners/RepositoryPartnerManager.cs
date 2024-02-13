using Domain.Entities.Partners;
using Domain.Repositories.Base;
using Domain.Repositories.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Partners
{
    public class RepositoryPartnerManager : IRepositoryPartnerManager
    {
        private readonly Lazy<IRepositoryEntityBase<Partner>> _repositoryPartner;
        private readonly Lazy<IRepositoryEntityBase<PartnerAreaWorkgroup>> _repositoryPartnerAreaWorkgroup;
        private readonly Lazy<IRepositoryEntityBase<PartnerContact>> _repositoryPartnerContact;
        private readonly Lazy<IRepositoryEntityBase<BatchPartnerInvoice>> _repositoryPartnerBatchInvoice;

        public RepositoryPartnerManager(SmartDriveContext _context)
        {
            _repositoryPartner = new Lazy<IRepositoryEntityBase<Partner>>(() => new RepositoryPartner(_context));
            _repositoryPartnerAreaWorkgroup = new Lazy<IRepositoryEntityBase<PartnerAreaWorkgroup>>(() => new RepositoryPartnerAreaWorkgroup(_context));
            _repositoryPartnerContact = new Lazy<IRepositoryEntityBase<PartnerContact>>(() => new RepositoryPartnerContact(_context));
            _repositoryPartnerBatchInvoice = new Lazy<IRepositoryEntityBase<BatchPartnerInvoice>>(() => new RepositoryPartnerBatchInvoice(_context));
        }

        public IRepositoryEntityBase<Partner> RepositoryPartner => _repositoryPartner.Value;
        public IRepositoryEntityBase<PartnerAreaWorkgroup> RepositoryPartnerAreaWorkgroup => _repositoryPartnerAreaWorkgroup.Value;
        public IRepositoryEntityBase<PartnerContact> RepositoryPartnerContact => _repositoryPartnerContact.Value;
        public IRepositoryEntityBase<BatchPartnerInvoice> RepositoryPartnerBatchInvoice => _repositoryPartnerBatchInvoice.Value;
    }
}

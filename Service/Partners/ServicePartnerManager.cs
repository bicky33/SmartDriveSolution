using Contract.DTO.SO;
using Domain.Repositories.Partners;
using Domain.Repositories.SO;
using Service.Abstraction.Helpers;
using Service.Abstraction.Partners;
using Service.Abstraction.SO;
using Service.Helpers;
using Service.SO;

namespace Service.Partners
{
    public class ServicePartnerManager : IServicePartnerManager
    {
        private readonly Lazy<IFileServer> _fileServer;

        private readonly Lazy<IServicePartner> _servicePartner;
        private readonly Lazy<IServicePartnerAreaWorkgroup> _servicePartnerAreaWorkgroup;
        private readonly Lazy<IServicePartnerContact> _servicePartnerContact;
        private readonly Lazy<IServicePartnerClaimAssetSparepartBatch> _servicePartnerClaimAssetSparepartBatch;
        private readonly Lazy<IServicePartnerWorkOrder> _servicePartnerWorkOrder;

        private readonly Lazy<IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int>> _servicePartnerClaimAssetSparepart;
        private readonly Lazy<IServicePartnerClaimAssetEvidence> _servicePartnerClaimAssetEvidence;
        public ServicePartnerManager(IRepositoryPartnerManager _repositoryPartnerManager, IRepositorySOManager _repositorySOManager)
        {
            _fileServer = new Lazy<IFileServer>(() => new FileServer());

            _servicePartner = new Lazy<IServicePartner>(() => new ServicePartner(_repositoryPartnerManager));
            _servicePartnerAreaWorkgroup = new Lazy<IServicePartnerAreaWorkgroup>(() => new ServicePartnerAreaWorkgroup(_repositoryPartnerManager));
            _servicePartnerContact = new Lazy<IServicePartnerContact>(() => new ServicePartnerContact(_repositoryPartnerManager));
            _servicePartnerClaimAssetSparepartBatch = new Lazy<IServicePartnerClaimAssetSparepartBatch>(() => new ServicePartnerClaimAssetSparepartBatch(_repositoryPartnerManager));
            _servicePartnerWorkOrder = new Lazy<IServicePartnerWorkOrder>(() => new ServicePartnerWorkOrder(_repositoryPartnerManager));

            _servicePartnerClaimAssetSparepart = new Lazy<IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int>>(() => new ClaimAssetSparepartService(_repositorySOManager));
            _servicePartnerClaimAssetEvidence = new Lazy<IServicePartnerClaimAssetEvidence>(() => new ClaimAssetEvidenceService(_repositorySOManager, _fileServer.Value));
        }

        public IServicePartner ServicePartner => _servicePartner.Value;
        public IServicePartnerAreaWorkgroup ServicePartnerAreaWorkgroup => _servicePartnerAreaWorkgroup.Value;
        public IServicePartnerContact ServicePartnerContact => _servicePartnerContact.Value;
        public IServicePartnerClaimAssetSparepartBatch ServicePartnerClaimAssetSparepartBatch => _servicePartnerClaimAssetSparepartBatch.Value;

        public IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int> ServicePartnerClaimAssetSparepart => _servicePartnerClaimAssetSparepart.Value;
        public IServicePartnerClaimAssetEvidence ServicePartnerClaimAssetEvidence => _servicePartnerClaimAssetEvidence.Value;

        public IServicePartnerWorkOrder ServicePartnerWorkOrder => _servicePartnerWorkOrder.Value;
    }
}

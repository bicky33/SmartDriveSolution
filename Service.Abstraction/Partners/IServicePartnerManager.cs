using Contract.DTO.SO;
using Service.Abstraction.SO;

namespace Service.Abstraction.Partners
{
    public interface IServicePartnerManager
    {
        public IServicePartner ServicePartner { get; }
        public IServicePartnerAreaWorkgroup ServicePartnerAreaWorkgroup { get; }
        public IServicePartnerContact ServicePartnerContact { get; }
        public IServicePartnerClaimAssetSparepartBatch ServicePartnerClaimAssetSparepartBatch { get; }
        public IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int> ServicePartnerClaimAssetSparepart { get; }
        public IServicePartnerWorkOrder ServicePartnerWorkOrder { get; }
        public IServicePartnerClaimAssetEvidence ServicePartnerClaimAssetEvidence { get; }
    }
}

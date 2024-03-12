using Domain.Entities.SO;
using Domain.Repositories.Partners;

namespace Domain.Repositories.SO
{
    public interface IRepositorySOManager
    {
        IRepositorySOEntityBase<Service, int> ServiceRepository { get; }
        IRepositorySOEntityBase<ServiceOrder, string> ServiceOrderRepository { get; }
        IRepositorySOEntityBase<ServiceOrderTask, int> ServiceOrderTaskRepository { get; }
        IRepositorySOEntityBase<ServiceOrderWorkorder, int> ServiceOrderWorkorderRepository { get; }
        IRepositorySOEntityBase<ClaimAssetEvidence, int> ClaimAssetEvidenceRepository { get; }
        IRepositorySOEntityBase<ClaimAssetSparepart, int> ClaimAssetSparepartRepository { get; }
        IRepositorySOEntityBase<ServicePremi, int> ServicePremiRepository { get; }
        IRepositorySOEntityBase<ServicePremiCredit, int> ServicePremiCreditRepository { get; }
        IRepositoryPartnerClaimAssetEvidenceBatch RepositoryPartnerClaimAssetEvidenceBatch { get; }

        IUnitOfWorksSO UnitOfWork { get; }

    }
}

using Domain.Entities.SO;
using Domain.Repositories.Partners;
using static Domain.Repositories.SO.IRepositoryWithPagingSO;

namespace Domain.Repositories.SO
{
    public interface IRepositorySOManager
    {
        IRepositorySOEntityBase<Service, int> ServiceRepository { get; }
        IRepositoryWithPagingSO<ServiceOrder, string> ServiceOrderRepository { get; }
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

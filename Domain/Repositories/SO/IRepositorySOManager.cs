using Domain.Entities.SO;
using Domain.Repositories.SO;

namespace Domain.Repositories.SO
{
    public interface IRepositorySOManager
    {
        IRepositoryRelationSOBase<Service,int> ServiceRepository { get; }
        IRepositorySOEntityBase<ServiceOrder,string> ServiceOrderRepository { get; }
        IRepositoryRelationSOBase<ServiceOrderTask,int> ServiceOrderTaskRepository { get; }
        IRepositorySOEntityBase<ServiceOrderWorkorder,int> ServiceOrderWorkorderRepository { get; }
        IRepositorySOEntityBase<ClaimAssetEvidence,int> ClaimAssetEvidenceRepository { get; }
        IRepositorySOEntityBase<ClaimAssetSparepart,int> ClaimAssetSparepartRepository { get; }
        IRepositorySOEntityBase<ServicePremi, int> ServicePremiRepository { get; }
        IRepositorySOEntityBase<ServicePremiCredit, int> ServicePremiCreditRepository { get; }
        IUnitOfWorksSO UnitOfWork { get; }

    }
}

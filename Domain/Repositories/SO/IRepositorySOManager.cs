using Domain.Entities.SO;
using Domain.Repositories.SO;

namespace Domain.Repositories.SO
{
    public interface IRepositorySOManager
    {
        IRepositorySOEntityBase<Service,int> ServiceRepository { get; }
        IRepositorySOEntityBase<ServiceOrder,string> ServiceOrderRepository { get; }
        IRepositorySOEntityBase<ServiceOrderTask,int> ServiceOrderTaskRepository { get; }
        IRepositorySOEntityBase<ServiceOrderWorkorder,int> ServiceOrderWorkorderRepository { get; }
        IRepositorySOEntityBase<ClaimAssetEvidence,int> ClaimAssetEvidenceRepository { get; }
        IRepositorySOEntityBase<ClaimAssetSparepart,int> ClaimAssetSparepartRepository { get; }
        IUnitOfWorksSO UnitOfWork { get; }

    }
}

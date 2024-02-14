using Domain.Entities.SO;
using Domain.Repositories.SO;

namespace Domain.Repositories.SO
{
    public interface IRepositoryManager
    {
        IRepositoryEntityBase<Service,int> ServiceRepository { get; }
        IRepositoryEntityBase<ServiceOrder,string> ServiceOrderRepository { get; }
        IRepositoryEntityBase<ServiceOrderTask,int> ServiceOrderTaskRepository { get; }
        IRepositoryEntityBase<ServiceOrderWorkorder,int> ServiceOrderWorkorderRepository { get; }
        IUnitOfWorks UnitOfWork { get; }

    }
}

using Domain.Entities.Master;
using Domain.Entities.SO;
using Domain.Repositories.SO;
using Persistence.Repositories;
using Persistence.Repositories.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.SO
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IRepositoryEntityBase<Service,int>> _serviceRepository;
        private readonly Lazy<IRepositoryEntityBase<ServiceOrder,string>> _serviceOrderRepository;
        private readonly Lazy<IRepositoryEntityBase<ServiceOrderTask,int>> _serviceOrderTaskRepository;
        private readonly Lazy<IRepositoryEntityBase<ServiceOrderWorkorder,int>> _serviceOrderWorkorderRepository;
        private readonly Lazy<IUnitOfWorks> _unitOfWork;

        public RepositoryManager(SmartDriveContext dbContext)
        {
            _unitOfWork = new Lazy<IUnitOfWorks>(() => new UnitOfWorks(dbContext));
            _serviceRepository = new Lazy<IRepositoryEntityBase<Service,int>>(()=>new ServiceRepository(dbContext));
            _serviceOrderRepository = new Lazy<IRepositoryEntityBase<ServiceOrder,string>>(()=>new ServiceOrderRepository(dbContext));
            _serviceOrderTaskRepository = new Lazy<IRepositoryEntityBase<ServiceOrderTask,int>>(()=>new ServiceOrderTaskRepository(dbContext));
            _serviceOrderWorkorderRepository = new Lazy<IRepositoryEntityBase<ServiceOrderWorkorder,int>>(()=>new ServiceOrderWorkorderRepository(dbContext));

            
        }
        public IUnitOfWorks UnitOfWork => _unitOfWork.Value;

        public IRepositoryEntityBase<Service,int> ServiceRepository => _serviceRepository.Value;
        public IRepositoryEntityBase<ServiceOrder,string> ServiceOrderRepository => _serviceOrderRepository.Value;
        public IRepositoryEntityBase<ServiceOrderTask,int> ServiceOrderTaskRepository => _serviceOrderTaskRepository.Value;
        public IRepositoryEntityBase<ServiceOrderWorkorder,int> ServiceOrderWorkorderRepository => _serviceOrderWorkorderRepository.Value;
    }
}

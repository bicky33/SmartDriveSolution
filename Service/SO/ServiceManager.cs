using Contract.DTO.SO;
using Domain.Repositories.SO;
using Service.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Abstraction.SO;
using ServiceOrderTask.SO;


namespace Service.Base
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IServiceEntityBase<ServiceDto, ServiceDtoCreate, int>> _serviceService;
        private readonly Lazy<IServiceEntityBase<ServiceOrderDto, ServiceOrderDtoCreate, string>> _serviceOrderService;
        private readonly Lazy<IServiceEntityBase<ServiceOrderTaskDto, ServiceOrderTaskDtoCreate, int>> _serviceOrderTaskService;
        private readonly Lazy<IServiceEntityBase<ServiceOrderWorkorderDto, ServiceOrderWorkorderDtoCreate, int>> _serviceOrderWorkorderService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _serviceService = new Lazy<IServiceEntityBase<ServiceDto,ServiceDtoCreate,int>>(()=> new ServiceService(repositoryManager));
            _serviceOrderService = new Lazy<IServiceEntityBase<ServiceOrderDto, ServiceOrderDtoCreate, string>>(()=> new ServiceOrderService(repositoryManager));
            _serviceOrderTaskService = new Lazy<IServiceEntityBase<ServiceOrderTaskDto, ServiceOrderTaskDtoCreate, int>>(()=> new ServiceOrderTaskService(repositoryManager));
            _serviceOrderWorkorderService = new Lazy<IServiceEntityBase<ServiceOrderWorkorderDto, ServiceOrderWorkorderDtoCreate, int>>(()=> new ServiceOrderWorkorderService(repositoryManager));
        }

        public IServiceEntityBase<ServiceDto,ServiceDtoCreate,int> ServiceService => _serviceService.Value;
        public IServiceEntityBase<ServiceOrderDto,ServiceOrderDtoCreate,string> ServiceOrderService => _serviceOrderService.Value;
        public IServiceEntityBase<ServiceOrderTaskDto,ServiceOrderTaskDtoCreate, int> ServiceOrderTaskService => _serviceOrderTaskService.Value;
        public IServiceEntityBase<ServiceOrderWorkorderDto,ServiceOrderWorkorderDtoCreate, int> ServiceOrderWorkorderService => _serviceOrderWorkorderService.Value;

    }
}

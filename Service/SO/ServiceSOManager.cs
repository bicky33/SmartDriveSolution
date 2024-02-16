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


namespace Service.SO
{
    public class ServiceSOManager : IServiceSOManager
    {
        private readonly Lazy<IServiceSOEntityBase<ServiceDto, ServiceDtoCreate, int>> _serviceService;
        private readonly Lazy<IServiceSOEntityBase<ServiceOrderDto, ServiceOrderDtoCreate, string>> _serviceOrderService;
        private readonly Lazy<IServiceSOEntityBase<ServiceOrderTaskDto, ServiceOrderTaskDtoCreate, int>> _serviceOrderTaskService;
        private readonly Lazy<IServiceSOEntityBase<ServiceOrderWorkorderDto, ServiceOrderWorkorderDtoCreate, int>> _serviceOrderWorkorderService;
        private readonly Lazy<IServiceSOEntityBase<ClaimAssetEvidenceDto, ClaimAssetEvidenceDtoCreate, int>> _claimAssetEvidenceService;
        private readonly Lazy<IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int>> _claimAssetSparepartService;

        public ServiceSOManager(IRepositorySOManager repositoryManager)
        {
            _serviceService = new Lazy<IServiceSOEntityBase<ServiceDto,ServiceDtoCreate,int>>(()=> new ServiceService(repositoryManager));
            _serviceOrderService = new Lazy<IServiceSOEntityBase<ServiceOrderDto, ServiceOrderDtoCreate, string>>(()=> new ServiceOrderService(repositoryManager));
            _serviceOrderTaskService = new Lazy<IServiceSOEntityBase<ServiceOrderTaskDto, ServiceOrderTaskDtoCreate, int>>(()=> new ServiceOrderTaskService(repositoryManager));
            _serviceOrderWorkorderService = new Lazy<IServiceSOEntityBase<ServiceOrderWorkorderDto, ServiceOrderWorkorderDtoCreate, int>>(()=> new ServiceOrderWorkorderService(repositoryManager));
            _claimAssetEvidenceService = new Lazy<IServiceSOEntityBase<ClaimAssetEvidenceDto, ClaimAssetEvidenceDtoCreate, int>>(()=> new ClaimAssetEvidenceService(repositoryManager));
            _claimAssetSparepartService = new Lazy<IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int>>(()=> new ClaimAssetSparepartService(repositoryManager));
        }

        public IServiceSOEntityBase<ServiceDto,ServiceDtoCreate,int> ServiceService => _serviceService.Value;
        public IServiceSOEntityBase<ServiceOrderDto,ServiceOrderDtoCreate,string> ServiceOrderService => _serviceOrderService.Value;
        public IServiceSOEntityBase<ServiceOrderTaskDto,ServiceOrderTaskDtoCreate, int> ServiceOrderTaskService => _serviceOrderTaskService.Value;
        public IServiceSOEntityBase<ServiceOrderWorkorderDto,ServiceOrderWorkorderDtoCreate, int> ServiceOrderWorkorderService => _serviceOrderWorkorderService.Value;

        public IServiceSOEntityBase<ClaimAssetEvidenceDto, ClaimAssetEvidenceDtoCreate, int> ClaimAssetEvidenceService => _claimAssetEvidenceService.Value;

        public IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int> ClaimAssetSparepartService => _claimAssetSparepartService.Value;
    }
}

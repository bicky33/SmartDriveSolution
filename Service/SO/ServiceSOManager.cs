using Contract.DTO.SO;
using Domain.Repositories.SO;
using Service.Abstraction.Master;
using Service.Abstraction.Payment;
using Service.Abstraction.SO;

namespace Service.SO
{
    public class ServiceSOManager : IServiceSOManager
    {
        private readonly Lazy<IServiceSORelationBase<ServiceDto, ServiceDtoCreate, int>> _serviceService;
        private readonly Lazy<IServiceSOEntityBase<ServiceOrderDto, ServiceOrderDtoCreate, string>> _serviceOrderService;
        private readonly Lazy<IServiceSOEntityBase<ServiceOrderTaskDto, ServiceOrderTaskDtoCreate, int>> _serviceOrderTaskService;
        private readonly Lazy<IServiceSOEntityBase<ServiceOrderWorkorderDto, ServiceOrderWorkorderDtoCreate, int>> _serviceOrderWorkorderService;
        private readonly Lazy<IServiceSOEntityBase<ClaimAssetEvidenceDto, ClaimAssetEvidenceDtoCreate, int>> _claimAssetEvidenceService;
        private readonly Lazy<IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int>> _claimAssetSparepartService;
        private readonly Lazy<IServiceSOEntityBase<ServicePremiDto, ServicePremiDtoCreate, int>> _servicePremiService;
        private readonly Lazy<IServiceSOEntityBase<ServicePremiCreditDto, ServicePremiCreditDtoCreate, int>> _servicePremiCreditService;

        public ServiceSOManager(IRepositorySOManager repositoryManager, IServiceManagerMaster serviceManagerMaster,IServicePaymentManager servicePaymentManager)
        {
            _serviceService = new Lazy<IServiceSORelationBase<ServiceDto,ServiceDtoCreate,int>>(()=> new ServiceService(repositoryManager, serviceManagerMaster, servicePaymentManager));
            _serviceOrderService = new Lazy<IServiceSOEntityBase<ServiceOrderDto, ServiceOrderDtoCreate, string>>(()=> new ServiceOrderService(repositoryManager));
            _serviceOrderTaskService = new Lazy<IServiceSOEntityBase<ServiceOrderTaskDto, ServiceOrderTaskDtoCreate, int>>(()=> new ServiceOrderTaskService(repositoryManager));
            _serviceOrderWorkorderService = new Lazy<IServiceSOEntityBase<ServiceOrderWorkorderDto, ServiceOrderWorkorderDtoCreate, int>>(()=> new ServiceOrderWorkorderService(repositoryManager));
            //_claimAssetEvidenceService = new Lazy<IServiceSOEntityBase<ClaimAssetEvidenceDto, ClaimAssetEvidenceDtoCreate, int>>(()=> new ClaimAssetEvidenceService(repositoryManager));
            _claimAssetSparepartService = new Lazy<IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int>>(()=> new ClaimAssetSparepartService(repositoryManager));
            _servicePremiService = new Lazy<IServiceSOEntityBase<ServicePremiDto, ServicePremiDtoCreate, int>>(() => new ServicePremiService(repositoryManager));
            _servicePremiCreditService = new Lazy<IServiceSOEntityBase<ServicePremiCreditDto, ServicePremiCreditDtoCreate, int>>(() => new ServicePremiCreditService(repositoryManager));
        }

        public IServiceSORelationBase<ServiceDto,ServiceDtoCreate,int> ServiceService => _serviceService.Value;
        public IServiceSOEntityBase<ServiceOrderDto,ServiceOrderDtoCreate,string> ServiceOrderService => _serviceOrderService.Value;
        public IServiceSOEntityBase<ServiceOrderTaskDto,ServiceOrderTaskDtoCreate, int> ServiceOrderTaskService => _serviceOrderTaskService.Value;
        public IServiceSOEntityBase<ServiceOrderWorkorderDto,ServiceOrderWorkorderDtoCreate, int> ServiceOrderWorkorderService => _serviceOrderWorkorderService.Value;

        public IServiceSOEntityBase<ClaimAssetEvidenceDto, ClaimAssetEvidenceDtoCreate, int> ClaimAssetEvidenceService => _claimAssetEvidenceService.Value;

        public IServiceSOEntityBase<ClaimAssetSparepartDto, ClaimAssetSparepartDtoCreate, int> ClaimAssetSparepartService => _claimAssetSparepartService.Value;

        public IServiceSOEntityBase<ServicePremiDto, ServicePremiDtoCreate, int> ServicePremiService => _servicePremiService.Value;

        public IServiceSOEntityBase<ServicePremiCreditDto, ServicePremiCreditDtoCreate, int> ServicePremiCreditService => _servicePremiCreditService.Value;
    }
}

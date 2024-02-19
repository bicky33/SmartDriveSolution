using Domain.Entities.SO;
using Domain.Repositories.SO;


namespace Persistence.Repositories.SO
{
    public class RepositorySOManager : IRepositorySOManager
    {
        private readonly Lazy<IRepositorySOEntityBase<Service, int>> _serviceRepository;
        private readonly Lazy<IRepositorySOEntityBase<ServiceOrder, string>> _serviceOrderRepository;
        private readonly Lazy<IRepositorySOEntityBase<ServiceOrderTask, int>> _serviceOrderTaskRepository;
        private readonly Lazy<IRepositorySOEntityBase<ServiceOrderWorkorder, int>> _serviceOrderWorkorderRepository;
        private readonly Lazy<IRepositorySOEntityBase<ClaimAssetEvidence, int>> _claimAssetEvidenceRepository;
        private readonly Lazy<IRepositorySOEntityBase<ClaimAssetSparepart, int>> _claimAssetSparepartRepository;
        private readonly Lazy<IUnitOfWorksSO> _unitOfWork;

        public RepositorySOManager(SmartDriveContext dbContext)
        {
            _unitOfWork = new Lazy<IUnitOfWorksSO>(() => new UnitOfWorksSO(dbContext));
            _serviceRepository = new Lazy<IRepositorySOEntityBase<Service, int>>(() => new ServiceRepository(dbContext));
            _serviceOrderRepository = new Lazy<IRepositorySOEntityBase<ServiceOrder, string>>(() => new ServiceOrderRepository(dbContext));
            _serviceOrderTaskRepository = new Lazy<IRepositorySOEntityBase<ServiceOrderTask, int>>(() => new ServiceOrderTaskRepository(dbContext));
            _serviceOrderWorkorderRepository = new Lazy<IRepositorySOEntityBase<ServiceOrderWorkorder, int>>(() => new ServiceOrderWorkorderRepository(dbContext));
            _claimAssetEvidenceRepository = new Lazy<IRepositorySOEntityBase<ClaimAssetEvidence, int>>(() => new ClaimAssetEvidenceRepository(dbContext));
            _claimAssetSparepartRepository = new Lazy<IRepositorySOEntityBase<ClaimAssetSparepart, int>>(() => new ClaimAssetSparepartRepository(dbContext));


        }
        public IUnitOfWorksSO UnitOfWork => _unitOfWork.Value;

        public IRepositorySOEntityBase<Service, int> ServiceRepository => _serviceRepository.Value;
        public IRepositorySOEntityBase<ServiceOrder, string> ServiceOrderRepository => _serviceOrderRepository.Value;
        public IRepositorySOEntityBase<ServiceOrderTask, int> ServiceOrderTaskRepository => _serviceOrderTaskRepository.Value;
        public IRepositorySOEntityBase<ServiceOrderWorkorder, int> ServiceOrderWorkorderRepository => _serviceOrderWorkorderRepository.Value;

        public IRepositorySOEntityBase<ClaimAssetEvidence, int> ClaimAssetEvidenceRepository => _claimAssetEvidenceRepository.Value;

        public IRepositorySOEntityBase<ClaimAssetSparepart, int> ClaimAssetSparepartRepository => _claimAssetSparepartRepository.Value;
    }
}

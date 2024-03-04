using Domain.Entities.SO;
using Domain.Repositories.Partners;
using Domain.Repositories.SO;
using Persistence.Repositories;
using Persistence.Repositories.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.SO
{
    public class RepositorySOManager : IRepositorySOManager
    {
        private readonly Lazy<IRepositorySOEntityBase<Service,int>> _serviceRepository;
        private readonly Lazy<IRepositorySOEntityBase<ServiceOrder,string>> _serviceOrderRepository;
        private readonly Lazy<IRepositorySOEntityBase<ServiceOrderTask,int>> _serviceOrderTaskRepository;
        private readonly Lazy<IRepositorySOEntityBase<ServiceOrderWorkorder,int>> _serviceOrderWorkorderRepository;
        private readonly Lazy<IRepositorySOEntityBase<ClaimAssetEvidence,int>> _claimAssetEvidenceRepository;
        private readonly Lazy<IRepositorySOEntityBase<ClaimAssetSparepart,int>> _claimAssetSparepartRepository;
        private readonly Lazy<IRepositorySOEntityBase<ServicePremi, int>> _servicePremiRepository;
        private readonly Lazy<IRepositorySOEntityBase<ServicePremiCredit, int>> _servicePremiCreditRepository;
        private readonly Lazy<IRepositoryPartnerClaimAssetEvidenceBatch> _repositoryPartnerClaimAssetEvidenceBatch;
        private readonly Lazy<IUnitOfWorksSO> _unitOfWork;

        public RepositorySOManager(SmartDriveContext dbContext)
        {
            _unitOfWork = new Lazy<IUnitOfWorksSO>(() => new UnitOfWorksSO(dbContext));
            _serviceRepository = new Lazy<IRepositorySOEntityBase<Service,int>>(()=>new ServiceRepository(dbContext));
            _serviceOrderRepository = new Lazy<IRepositorySOEntityBase<ServiceOrder,string>>(()=>new ServiceOrderRepository(dbContext));
            _serviceOrderTaskRepository = new Lazy<IRepositorySOEntityBase<ServiceOrderTask,int>>(()=>new ServiceOrderTaskRepository(dbContext));
            _serviceOrderWorkorderRepository = new Lazy<IRepositorySOEntityBase<ServiceOrderWorkorder,int>>(()=>new ServiceOrderWorkorderRepository(dbContext));
            _claimAssetEvidenceRepository = new Lazy<IRepositorySOEntityBase<ClaimAssetEvidence, int>>(()=>new ClaimAssetEvidenceRepository(dbContext));
            _claimAssetSparepartRepository = new Lazy<IRepositorySOEntityBase<ClaimAssetSparepart, int>>(()=>new ClaimAssetSparepartRepository(dbContext));
            _servicePremiRepository = new Lazy<IRepositorySOEntityBase<ServicePremi, int>>(() => new ServicePremiRepository(dbContext));
            _servicePremiCreditRepository = new Lazy<IRepositorySOEntityBase<ServicePremiCredit, int>>(() => new ServicePremiCreditRepository(dbContext));
            _repositoryPartnerClaimAssetEvidenceBatch = new Lazy<IRepositoryPartnerClaimAssetEvidenceBatch>(() => new RepositoryPartnerClaimAssetEvidenceBatch(dbContext));


        }
        public IUnitOfWorksSO UnitOfWork => _unitOfWork.Value;

        public IRepositorySOEntityBase<Service,int> ServiceRepository => _serviceRepository.Value;
        public IRepositorySOEntityBase<ServiceOrder,string> ServiceOrderRepository => _serviceOrderRepository.Value;
        public IRepositorySOEntityBase<ServiceOrderTask,int> ServiceOrderTaskRepository => _serviceOrderTaskRepository.Value;
        public IRepositorySOEntityBase<ServiceOrderWorkorder,int> ServiceOrderWorkorderRepository => _serviceOrderWorkorderRepository.Value;

        public IRepositorySOEntityBase<ClaimAssetEvidence, int> ClaimAssetEvidenceRepository => _claimAssetEvidenceRepository.Value;

        public IRepositorySOEntityBase<ClaimAssetSparepart, int> ClaimAssetSparepartRepository => _claimAssetSparepartRepository.Value;

        public IRepositorySOEntityBase<ServicePremi, int> ServicePremiRepository => _servicePremiRepository.Value;

        public IRepositorySOEntityBase<ServicePremiCredit, int> ServicePremiCreditRepository => _servicePremiCreditRepository.Value;

        public IRepositoryPartnerClaimAssetEvidenceBatch RepositoryPartnerClaimAssetEvidenceBatch => _repositoryPartnerClaimAssetEvidenceBatch.Value;
    }
}
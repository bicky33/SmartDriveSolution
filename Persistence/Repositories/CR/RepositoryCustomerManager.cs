using Domain.Entities.CR;
using Domain.Repositories.Base;
using Domain.Repositories.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CR
{
    public class RepositoryCustomerManager : IRepositoryCustomerManager
    {
        private readonly Lazy<ICustomerUnitOfWork> _customerUnitOfWork;
        private readonly Lazy<ICustomerRequestRepository> _customerRequestRepository;
        private readonly Lazy<IRepositoryEntityBase<CustomerInscAsset>> _customerInscAssetRepository;
        private readonly Lazy<IRepositoryEntityBase<CustomerClaim>> _customerClaimRepository;
        private readonly Lazy<IRepositoryEntityBase<CustomerInscDoc>> _customerInscDocRepository;
        private readonly Lazy<IRepositoryEntityBase<CustomerInscExtend>> _customerInscExtendRepository;

        public RepositoryCustomerManager(SmartDriveContext dbContext)
        {
            _customerUnitOfWork = new Lazy<ICustomerUnitOfWork>(() => new CustomerUnitOfWork(dbContext));
            _customerRequestRepository = new Lazy<ICustomerRequestRepository>(() => new CustomerRequestRepository(dbContext));
            _customerInscAssetRepository = new Lazy<IRepositoryEntityBase<CustomerInscAsset>>(() => new CustomerInscAssetsRepository(dbContext));
            _customerClaimRepository = new Lazy<IRepositoryEntityBase<CustomerClaim>>(() => new CustomerClaimRepository(dbContext));
            _customerInscDocRepository = new Lazy<IRepositoryEntityBase<CustomerInscDoc>>(() => new CustomerInscDocRepository(dbContext));
            _customerInscExtendRepository = new Lazy<IRepositoryEntityBase<CustomerInscExtend>>(() =>  new CustomerInscExtendRepository(dbContext));
        }

        public ICustomerRequestRepository CustomerRequestRepository => _customerRequestRepository.Value;

        public ICustomerUnitOfWork CustomerUnitOfWork => _customerUnitOfWork.Value;

        public IRepositoryEntityBase<CustomerInscAsset> CustomerInscAssetRepository => _customerInscAssetRepository.Value;

        public IRepositoryEntityBase<CustomerClaim> CustomerClaimRepository => _customerClaimRepository.Value;

        public IRepositoryEntityBase<CustomerInscDoc> CustomerInscDocRepository => _customerInscDocRepository.Value;

        public IRepositoryEntityBase<CustomerInscExtend> CustomerInscExtendRepository => _customerInscExtendRepository.Value;
    }
}

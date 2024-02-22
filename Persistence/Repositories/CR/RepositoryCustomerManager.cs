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
        private readonly Lazy<ICustomerInscAssetRepository> _customerInscAssetRepository;
        private readonly Lazy<ICustomerClaimRepository> _customerClaimRepository;
        private readonly Lazy<IRepositoryEntityBase<CustomerInscDoc>> _customerInscDocRepository;
        private readonly Lazy<IRepositoryEntityBase<CustomerInscExtend>> _customerInscExtendRepository;

        public RepositoryCustomerManager(SmartDriveContext dbContext)
        {
            _customerUnitOfWork = new Lazy<ICustomerUnitOfWork>(() => new CustomerUnitOfWork(dbContext));
            _customerRequestRepository = new Lazy<ICustomerRequestRepository>(() => new CustomerRequestRepository(dbContext));
            _customerInscAssetRepository = new Lazy<ICustomerInscAssetRepository>(() => new CustomerInscAssetsRepository(dbContext));
            _customerClaimRepository = new Lazy<ICustomerClaimRepository>(() => new CustomerClaimRepository(dbContext));
            _customerInscDocRepository = new Lazy<IRepositoryEntityBase<CustomerInscDoc>>(() => new CustomerInscDocRepository(dbContext));
            _customerInscExtendRepository = new Lazy<IRepositoryEntityBase<CustomerInscExtend>>(() =>  new CustomerInscExtendRepository(dbContext));
        }

        public ICustomerRequestRepository CustomerRequestRepository => _customerRequestRepository.Value;
        public ICustomerUnitOfWork CustomerUnitOfWork => _customerUnitOfWork.Value;
        public IRepositoryEntityBase<CustomerInscDoc> CustomerInscDocRepository => _customerInscDocRepository.Value;
        public IRepositoryEntityBase<CustomerInscExtend> CustomerInscExtendRepository => _customerInscExtendRepository.Value;
        public ICustomerInscAssetRepository CustomerInscAssetRepository => _customerInscAssetRepository.Value;
        public ICustomerClaimRepository CustomerClaimRepository => _customerClaimRepository.Value;
    }
}

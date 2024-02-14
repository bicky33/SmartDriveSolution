using Domain.Entities.CR;
using Domain.Repositories.Base;
using Domain.Repositories.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CR
{
    public class RepositoryCustomerManager : IRepositoryCustomerManager
    {
        private readonly Lazy<ICustomerUnitOfWork> _customerUnitOfWork;
        private readonly Lazy<ICustomerRequestRepository> _customerRequestRepository;
        private readonly Lazy<IRepositoryEntityBase<CustomerInscAsset>> _customerInscAssetRepository;

        public RepositoryCustomerManager(SmartDriveContext dbContext)
        {
            _customerUnitOfWork = new Lazy<ICustomerUnitOfWork>(() => new CustomerUnitOfWork(dbContext));
            _customerRequestRepository = new Lazy<ICustomerRequestRepository>(() => new CustomerRequestRepository(dbContext));
            _customerInscAssetRepository = new Lazy<IRepositoryEntityBase<CustomerInscAsset>>(() => new CustomerInscAssetsRepository(dbContext));
        }

        public ICustomerRequestRepository CustomerRequestRepository => _customerRequestRepository.Value;

        public ICustomerUnitOfWork CustomerUnitOfWork => _customerUnitOfWork.Value;

        public IRepositoryEntityBase<CustomerInscAsset> CustomerInscAssetRepository => _customerInscAssetRepository.Value;
    }
}

using Domain.Repositories.CR;
using Domain.Repositories.UserModule;
using Service.Abstraction.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CR
{
    public class ServiceCustomerManager : IServiceCustomerManager
    {
        private readonly Lazy<ICustomerRequestService> _customerRequestService;
        private readonly Lazy<ICustomerInscAssetService> _customerInscAssetService;
        private readonly Lazy<ICustomerClaimService> _customerClaimService;
        public ServiceCustomerManager(IRepositoryCustomerManager customerRepositoryManager, IRepositoryManagerUser repositoryManagerUser)
        {
            _customerRequestService = new Lazy<ICustomerRequestService>(() => new CustomerRequestService(customerRepositoryManager, repositoryManagerUser));
            _customerInscAssetService = new Lazy<ICustomerInscAssetService>(() => new CustomerInscAssetsService(customerRepositoryManager));
            _customerClaimService = new Lazy<ICustomerClaimService>(() => new CustomerClaimService(customerRepositoryManager));
        }
        public ICustomerRequestService CustomerRequestService => _customerRequestService.Value;
        public ICustomerInscAssetService CustomerInscAssetService => _customerInscAssetService.Value;
        public ICustomerClaimService CustomerClaimService => _customerClaimService.Value;
    }
}

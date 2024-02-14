using Domain.Repositories.CR;
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
        public ServiceCustomerManager(IRepositoryCustomerManager customerRepositoryManager)
        {
            _customerRequestService = new Lazy<ICustomerRequestService>(() => new CustomerRequestService(customerRepositoryManager));
            _customerInscAssetService = new Lazy<ICustomerInscAssetService>(() => new CustomerInscAssetsService(customerRepositoryManager));
        }
        public ICustomerRequestService CustomerRequestService => _customerRequestService.Value;
        public ICustomerInscAssetService CustomerInscAssetService => _customerInscAssetService.Value;
    }
}

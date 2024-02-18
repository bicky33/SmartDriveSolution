using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Domain.Repositories.CR;
using Domain.Repositories.Master;
using Domain.Repositories.UserModule;
using Service.Abstraction.Base;
using Service.Abstraction.CR;
using Service.Abstraction.User;
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
        private readonly Lazy<IServiceEntityBase<CustomerInscDocDto>> _customerInscDocService;
        private readonly Lazy<ICustomerInscExtendService> _customerInscExtendService;
        public ServiceCustomerManager(
            IRepositoryCustomerManager customerRepositoryManager,
            IRepositoryManagerUser repositoryManagerUser,
            IRepositoryManagerMaster repositoryManagerMaster
            )
        {
            _customerRequestService = new Lazy<ICustomerRequestService>(() => new CustomerRequestService(customerRepositoryManager, repositoryManagerUser));
            _customerInscAssetService = new Lazy<ICustomerInscAssetService>(() => new CustomerInscAssetsService(customerRepositoryManager, repositoryManagerMaster));
            _customerClaimService = new Lazy<ICustomerClaimService>(() => new CustomerClaimService(customerRepositoryManager));
            _customerInscDocService = new Lazy<IServiceEntityBase<CustomerInscDocDto>>(() => new CustomerInscDocService(customerRepositoryManager));
            _customerInscExtendService = new Lazy<ICustomerInscExtendService>(() => new CustomerInscExtendService(customerRepositoryManager));
        }
        public ICustomerRequestService CustomerRequestService => _customerRequestService.Value;
        public ICustomerInscAssetService CustomerInscAssetService => _customerInscAssetService.Value;
        public ICustomerClaimService CustomerClaimService => _customerClaimService.Value;
        public IServiceEntityBase<CustomerInscDocDto> CustomerInscDocService => _customerInscDocService.Value;
        public ICustomerInscExtendService CustomerInscExtendService => _customerInscExtendService.Value;
    }
}

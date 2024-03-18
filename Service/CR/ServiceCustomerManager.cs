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
            IRepositoryCustomerManager repositoryCustomerManager,
            IRepositoryManagerUser repositoryManagerUser,
            IRepositoryManagerMaster repositoryManagerMaster
            )
        {
            _customerRequestService = new Lazy<ICustomerRequestService>(() => new CustomerRequestService(repositoryCustomerManager, repositoryManagerUser, repositoryManagerMaster));
            _customerInscAssetService = new Lazy<ICustomerInscAssetService>(() => new CustomerInscAssetsService(repositoryCustomerManager, repositoryManagerMaster));
            _customerClaimService = new Lazy<ICustomerClaimService>(() => new CustomerClaimService(repositoryCustomerManager));
            _customerInscDocService = new Lazy<IServiceEntityBase<CustomerInscDocDto>>(() => new CustomerInscDocService(repositoryCustomerManager));
            _customerInscExtendService = new Lazy<ICustomerInscExtendService>(() => new CustomerInscExtendService(repositoryCustomerManager));
        }
        public ICustomerRequestService CustomerRequestService => _customerRequestService.Value;
        public ICustomerInscAssetService CustomerInscAssetService => _customerInscAssetService.Value;
        public ICustomerClaimService CustomerClaimService => _customerClaimService.Value;
        public IServiceEntityBase<CustomerInscDocDto> CustomerInscDocService => _customerInscDocService.Value;
        public ICustomerInscExtendService CustomerInscExtendService => _customerInscExtendService.Value;
    }
}

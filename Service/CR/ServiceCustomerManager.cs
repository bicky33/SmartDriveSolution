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
        public ServiceCustomerManager(IRepositoryCustomerManager customerRepositoryManager)
        {
            _customerRequestService = new Lazy<ICustomerRequestService>(() => new CustomerRequestService(customerRepositoryManager));
        }
        public ICustomerRequestService CustomerRequestService => _customerRequestService.Value;
    }
}

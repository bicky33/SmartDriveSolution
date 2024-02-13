using Domain.Repositories.CR;
using Service.Abstraction.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CR
{
    public class CustomerServiceManager : ICustomerServiceManager
    {
        private readonly Lazy<ICustomerRequestService> _customerRequestService;
        public CustomerServiceManager(ICustomerRepositoryManager customerRepositoryManager)
        {
            _customerRequestService = new Lazy<ICustomerRequestService>(() => new CustomerRequestService(customerRepositoryManager));
        }
        public ICustomerRequestService CustomerRequestService => _customerRequestService.Value;
    }
}

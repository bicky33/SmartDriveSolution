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

        public RepositoryCustomerManager(SmartDriveContext dbContext)
        {
            _customerUnitOfWork = new Lazy<ICustomerUnitOfWork>(() => new CustomerUnitOfWork(dbContext));
            _customerRequestRepository = new Lazy<ICustomerRequestRepository>(() => new CustomerRequestRepository(dbContext));
        }

        public ICustomerRequestRepository CustomerRequestRepository => _customerRequestRepository.Value;

        public ICustomerUnitOfWork CustomerUnitOfWork => _customerUnitOfWork.Value;
    }
}

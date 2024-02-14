using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.CR
{
    public interface IRepositoryCustomerManager
    {
        ICustomerRequestRepository CustomerRequestRepository { get; }
        ICustomerUnitOfWork CustomerUnitOfWork { get; }
    }
}

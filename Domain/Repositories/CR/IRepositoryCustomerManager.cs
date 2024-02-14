using Domain.Entities.CR;
using Domain.Repositories.Base;
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
        IRepositoryEntityBase<CustomerInscAsset> CustomerInscAssetRepository { get; }
        ICustomerUnitOfWork CustomerUnitOfWork { get; }
    }
}

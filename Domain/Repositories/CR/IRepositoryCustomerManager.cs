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
        ICustomerInscAssetRepository CustomerInscAssetRepository { get; }
        ICustomerClaimRepository CustomerClaimRepository { get; }
        ICustomerUnitOfWork CustomerUnitOfWork { get; }
        IRepositoryEntityBase<CustomerInscDoc> CustomerInscDocRepository { get; }
        IRepositoryEntityBase<CustomerInscExtend> CustomerInscExtendRepository { get; }
    }
}

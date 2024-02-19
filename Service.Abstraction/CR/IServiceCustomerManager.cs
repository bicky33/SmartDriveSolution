using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.CR
{
    public interface IServiceCustomerManager
    {
        ICustomerRequestService CustomerRequestService { get; }
        ICustomerInscAssetService CustomerInscAssetService { get; }
        ICustomerClaimService CustomerClaimService { get; }
        IServiceEntityBase<CustomerInscDocDto> CustomerInscDocService { get; }
        ICustomerInscExtendService CustomerInscExtendService { get; }
    }
}

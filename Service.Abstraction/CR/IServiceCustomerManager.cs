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
    }
}

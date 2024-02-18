using Domain.Entities.CR;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.CR
{
    public interface ICustomerClaimRepository : IRepositoryEntityBase<CustomerClaim>
    {
        CustomerClaim CreateData(CustomerClaim entity);
    }
}

using Domain.Entities.CR;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.CR
{
    public interface ICustomerRequestRepository : IRepositoryEntityBase<CustomerRequest>
    {
        Task<IEnumerable<CustomerRequest>> GetAllByUserId(int userId, bool trackChanges);
        Task<IEnumerable<CustomerRequest>> GetAllByEmployee(string eawgCode, bool trackChanges);
    }
}

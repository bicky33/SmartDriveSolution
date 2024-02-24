using Domain.Entities.CR;
using Domain.Entities.Master;
using Domain.Repositories.Base;
using Domain.RequestFeatured;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.CR
{
    public interface ICustomerRequestRepository : IRepositoryEntityBase<CustomerRequest>
    {
        Task<IEnumerable<CustomerRequest>> GetAllByCustomer(int userId, bool trackChanges);
        Task<IEnumerable<CustomerRequest>> GetAllByEmployee(string eawgCode, bool trackChanges);
        Task<CustomerRequest> GetById(int id, bool trackChanges);
        Task<PagedList<CustomerRequest>> GetAllPaging(EntityParameter entityParams, bool trackChanges);
    }
}

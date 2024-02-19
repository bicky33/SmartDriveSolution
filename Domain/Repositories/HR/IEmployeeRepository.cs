using Domain.Entities.HR;
using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.RequestFeatured;

namespace Domain.Repositories.HR
{
    public interface IEmployeeRepository : IRepositoryEntityBase<Employee>
    {
        Task<PagedList<Employee>> GetAllPaging(EntityParameter entityParams, bool trackChanges); 
       /* void CreateEmployee(BusinessEntity be, User user, UserAddress userAddress, UserPhone userPhone, UserRole userRole, Employee employee); */

    }
}

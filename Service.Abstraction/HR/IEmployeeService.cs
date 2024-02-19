using Contract.DTO.HR;
using Contract.DTO.HR.CompositeDto;
using Domain.RequestFeatured;
using Service.Abstraction.Base;

namespace Service.Abstraction.HR
{
    public interface IEmployeeService : IServiceEntityBase<EmployeeDto>
    {
        Task<IEnumerable<EmployeeDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges);
        Task<BusinessEntityCompositeDto>  CreateEmployee(BusinessEntityCompositeDto entity);
    }
}

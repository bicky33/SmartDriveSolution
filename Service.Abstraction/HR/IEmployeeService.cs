using Contract.DTO.HR;
using Contract.DTO.HR.CompositeDto;
using Contract.DTO.HR.UpdateEmployee;
using Domain.RequestFeatured;
using Service.Abstraction.Base;

namespace Service.Abstraction.HR
{
    public interface IEmployeeService : IServiceEntityBase<EmployeeDto>
    {
        Task<IEnumerable<EmployeeShowDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges);
        Task<EmployeeCreateDto>CreateEmployee(EmployeeCreateDto entity);
        Task<IEnumerable<EmployeeShowDto>> GetData(bool trackChanges);
        Task UpdateData(int id, EmployeeUpdateDto entity);

        Task<EmployeeCreateDto> FindEmployeeById(int id);
    }
}

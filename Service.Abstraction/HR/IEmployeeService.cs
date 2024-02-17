using Contract.DTO.HR;
using Domain.Repositories.HR.RequestFeature;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.HR
{
    public interface IEmployeeService : IServiceEntityBase<EmployeeDto>
    {
        Task<IEnumerable<EmployeeDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges);
    }
}

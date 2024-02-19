using Contract.DTO.HR;
using Domain.RequestFeatured;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.HR
{
    public interface IEmployeeArwgService : IServiceEntityBase<EmployeeAreaWorkGroupDto>
    {
        Task<IEnumerable<EmployeeAreaWorkGroupDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges);
    }
}

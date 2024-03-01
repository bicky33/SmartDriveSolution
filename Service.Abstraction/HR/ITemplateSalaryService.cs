using Contract.DTO.HR;
using Domain.Entities.HR;
using Domain.RequestFeatured;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.HR
{
    public interface ITemplateSalaryService :IServiceEntityBase<TemplateSalaryDto>
    {
        Task<IEnumerable<TemplateSalaryDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges);
    }
}

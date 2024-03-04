using Contract.DTO.HR;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.HR
{
    public interface IJobTypeService : IServiceEntityBase<JobTypeDto>
    {
        Task<JobTypeDto> GetJobTypeById(string id, bool trackChanges);
        Task DeleteDataAsync(string id);
        Task UpdateDataAsync(string id, JobTypeDto entity);
    }
}

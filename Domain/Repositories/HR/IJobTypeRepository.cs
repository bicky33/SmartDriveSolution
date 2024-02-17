using Domain.Entities.HR;
using Domain.Entities.Master;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.HR
{
    public interface IJobTypeRepository : IRepositoryEntityBase<JobType>
    {
        Task<JobType> GetJobTypeById(string id, bool trackChanges);
    }
}

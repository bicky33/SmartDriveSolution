using Domain.Entities.HR;
using Domain.Entities.Master;
using Domain.Repositories.Base;
using Domain.Repositories.HR.RequestFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.HR
{
    public interface IEmployeeRepository : IRepositoryEntityBase<Employee>
    {
        Task<PagedList<Employee>> GetAllPaging(EntityParameter entityParams, bool trackChanges);
    }
}

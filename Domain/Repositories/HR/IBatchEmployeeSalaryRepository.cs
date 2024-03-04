using Domain.Entities.HR;
using Domain.Repositories.Base;
using Domain.RequestFeatured;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.HR
{
    public interface IBatchEmployeeSalaryRepository : IRepositoryEntityBase<BatchEmployeeSalary>
    {
        Task<PagedList<BatchEmployeeSalary>> GetAllPaging(EntityParameter entityParams, bool trackChanges);
    }
}

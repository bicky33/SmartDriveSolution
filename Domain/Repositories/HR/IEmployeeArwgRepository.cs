using Domain.Entities.HR;
using Domain.Entities.Master;
using Domain.Repositories.Base;
using Domain.RequestFeatured;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.HR
{
    public interface IEmployeeArwgRepository : IRepositoryEntityBase<EmployeeAreWorkgroup>
    {
        Task<PagedList<EmployeeAreWorkgroup>> GetAllPaging(EntityParameter entityParams, bool trackChanges);
        //Task<IEnumerable<Zone>> GetData();
        //Task<IEnumerable<EmployeeAreaWorkGroupShowDto>> GetAllData(bool trackChanges);
        Task<IEnumerable<EmployeeAreWorkgroup>> FindEmployeeById(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.HR
{
    public interface IRepositoryManager
    {
        IJobTypeRepository JobTypeRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }

        IUnitOfWorks UnitOfWorks { get; }
    }
}

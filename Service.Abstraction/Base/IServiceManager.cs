using Service.Abstraction.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Base
{
    public interface IServiceManager
    {
        IJobTypeService JobTypeService { get; }
        IEmployeeService EmployeeService { get; }
    }
}

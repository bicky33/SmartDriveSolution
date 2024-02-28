using Service.Abstraction.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.HR
{
    public interface IServiceHRManager
    {
        IJobTypeService JobTypeService { get; }
        IEmployeeService EmployeeService { get; }
        IEmployeeArwgService EmployeeArwgService { get; }
        IBatchEmployeeSalaryService BatchEmployeeSalaryService { get; }
        IEmployeeSalaryDetailService EmployeeSalaryDetailService { get; }
        ITemplateSalaryService TemplateSalaryService { get; }
/*        IServiceBusinessEntity BusinessEntityService { get; }
        IServiceUser UserService { get; }
        IServiceUserAddress UserAddressService { get; }
        IServiceUserRole UserRoleService { get; }
        IServiceUserPhone UserPhoneService { get; }*/
    }
}

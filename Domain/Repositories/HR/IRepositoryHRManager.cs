using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.Repositories.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.HR
{
    public interface IRepositoryHRManager
    {
        IJobTypeRepository JobTypeRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IEmployeeArwgRepository EmployeeArwgRepository { get; }
        IBatchEmployeeSalaryRepository BatchEmployeeSalaryRepository { get; }
        IEmployeeSalaryDetailRepository EmployeeSalaryDetailRepository { get; }
        ITemplateSalaryRepository TemplateSalaryRepository { get; }
        IRepositoryBusinessEntity<BusinessEntity> BusinessEntityRepository { get; }
        IUnitOfWorks UnitOfWorks { get; }
    }
}

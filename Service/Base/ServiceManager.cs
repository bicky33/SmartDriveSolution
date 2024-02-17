using Domain.Repositories.Base;
using Domain.Repositories.HR;
using Service.Abstraction.Base;
using Service.Abstraction.HR;
using Services.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Base
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IJobTypeService> _jobTypeService;
        private readonly Lazy<IEmployeeService> _employeeService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _jobTypeService = new Lazy<IJobTypeService>(() => new JobTypeService(repositoryManager));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager));
        }

        public IJobTypeService JobTypeService => _jobTypeService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;
    }
}

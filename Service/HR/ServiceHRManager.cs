using Domain.Repositories.HR;
using Service.Abstraction.HR;

namespace Service.HR
{
    public class ServiceHRManager : IServiceHRManager
    {
        private readonly Lazy<IJobTypeService> _jobTypeService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IEmployeeArwgService> _employeeArwgService;
        private readonly Lazy<IBatchEmployeeSalaryService> _batchEmployeeSalaryService;
        private readonly Lazy<IEmployeeSalaryDetailService> _employeeSalaryDetailService;
        private readonly Lazy<ITemplateSalaryService> _templateSalaryService;
        //isi private irepoUserManager 
        public ServiceHRManager(IRepositoryHRManager repositoryManager)
        {
            _jobTypeService = new Lazy<IJobTypeService>(() => new JobTypeService(repositoryManager));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager));
            _employeeArwgService = new Lazy<IEmployeeArwgService>(() => new EmployeeArwgService(repositoryManager));
            _batchEmployeeSalaryService = new Lazy<IBatchEmployeeSalaryService>(() => new BatchEmployeeSalaryService(repositoryManager));
           _employeeSalaryDetailService = new Lazy<IEmployeeSalaryDetailService>(() => new EmployeeSalaryDetailService(repositoryManager));
            _templateSalaryService = new Lazy<ITemplateSalaryService>(() => new TemplateSalaryService(repositoryManager));
        }

        public IJobTypeService JobTypeService => _jobTypeService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;

        public IEmployeeArwgService EmployeeArwgService => _employeeArwgService.Value;

        public IBatchEmployeeSalaryService BatchEmployeeSalaryService => _batchEmployeeSalaryService.Value;

        public IEmployeeSalaryDetailService EmployeeSalaryDetailService => _employeeSalaryDetailService.Value;

        public ITemplateSalaryService TemplateSalaryService => _templateSalaryService.Value;
    }
}

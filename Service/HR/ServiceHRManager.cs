using Domain.Repositories.Base;
using Domain.Repositories.HR;
using Domain.Repositories.UserModule;
using Service.Abstraction.HR;
using Service.Abstraction.User;
using Service.UserModule;

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

        // user
        /*private readonly Lazy<IServiceBusinessEntity> _businessEntityService;
        private readonly Lazy<IServiceUser> _userService;
        private readonly Lazy<IServiceUserAddress> _userAddressService;
        private readonly Lazy<IServiceUserPhone> _userPhoneService;
        private readonly Lazy<IServiceUserRole> _userRoleService;*/

        //isi private irepoUserManager 
        public ServiceHRManager(IRepositoryHRManager repositoryManager, IRepositoryManagerUser userRepositoryManager)
        {
            _jobTypeService = new Lazy<IJobTypeService>(() => new JobTypeService(repositoryManager));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repositoryManager, userRepositoryManager));
            _employeeArwgService = new Lazy<IEmployeeArwgService>(() => new EmployeeArwgService(repositoryManager));
            _batchEmployeeSalaryService = new Lazy<IBatchEmployeeSalaryService>(() => new BatchEmployeeSalaryService(repositoryManager));
           _employeeSalaryDetailService = new Lazy<IEmployeeSalaryDetailService>(() => new EmployeeSalaryDetailService(repositoryManager));
            _templateSalaryService = new Lazy<ITemplateSalaryService>(() => new TemplateSalaryService(repositoryManager));

            //User
           /* _businessEntityService = new Lazy<IServiceBusinessEntity>(() => new BusinessEntityService(repositoryManager));*/

        }

        public IJobTypeService JobTypeService => _jobTypeService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;

        public IEmployeeArwgService EmployeeArwgService => _employeeArwgService.Value;

        public IBatchEmployeeSalaryService BatchEmployeeSalaryService => _batchEmployeeSalaryService.Value;

        public IEmployeeSalaryDetailService EmployeeSalaryDetailService => _employeeSalaryDetailService.Value;

        public ITemplateSalaryService TemplateSalaryService => _templateSalaryService.Value;

/*        public IServiceBusinessEntity BusinessEntityService => throw new NotImplementedException();

        public IServiceUser UserService => throw new NotImplementedException();

        public IServiceUserAddress UserAddressService => throw new NotImplementedException();

        public IServiceUserRole UserRoleService => throw new NotImplementedException();

        public IServiceUserPhone UserPhoneService => throw new NotImplementedException();*/
    }
}

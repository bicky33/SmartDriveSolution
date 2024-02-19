using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.Repositories.HR;
using Domain.Repositories.UserModule;
using Persistence.Repositories;
using Persistence.Repositories.UserModule;

namespace Persistence.Repositories.HR
{
    public class RepositoryHRManager : IRepositoryHRManager
    {
        private readonly Lazy<IUnitOfWorks> _unitOfWorks;
        private readonly Lazy<IJobTypeRepository> _jobTypeRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IEmployeeArwgRepository> _employeeArwgRepository;
        private readonly Lazy<IBatchEmployeeSalaryRepository> _batchEmployeeSalaryRepository;
        private readonly Lazy<IEmployeeSalaryDetailRepository> _employeeSalaryDetailRepository;
        private readonly Lazy<ITemplateSalaryRepository> _templateSalaryRepository;
        private readonly Lazy<IRepositoryBusinessEntity<BusinessEntity>> _businessEntity;
        public RepositoryHRManager(SmartDriveContext dbContext)
        {
            _jobTypeRepository = new Lazy<IJobTypeRepository>(() => new JobTypeRepository(dbContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
            _employeeArwgRepository = new Lazy<IEmployeeArwgRepository> (()=> new EmployeeArwgRepository(dbContext));
            _batchEmployeeSalaryRepository = new Lazy<IBatchEmployeeSalaryRepository>(() => new BatchEmployeeSalaryRepository(dbContext));
            _employeeSalaryDetailRepository = new Lazy<IEmployeeSalaryDetailRepository>(() => new EmployeeSalaryDetailRepository(dbContext));
            _templateSalaryRepository = new Lazy<ITemplateSalaryRepository>(() => new TemplateSalaryRepository(dbContext));
            _businessEntity = new Lazy<IRepositoryBusinessEntity<BusinessEntity>>(() => new BusinessEntityRepository(dbContext));

            _unitOfWorks = new Lazy<IUnitOfWorks>(() => new UnitOfWorks(dbContext));
        }

        public IJobTypeRepository JobTypeRepository => _jobTypeRepository.Value;

        public IUnitOfWorks UnitOfWorks => _unitOfWorks.Value;

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IEmployeeArwgRepository EmployeeArwgRepository => _employeeArwgRepository.Value;

        public IBatchEmployeeSalaryRepository BatchEmployeeSalaryRepository => _batchEmployeeSalaryRepository.Value;

        public IEmployeeSalaryDetailRepository EmployeeSalaryDetailRepository => _employeeSalaryDetailRepository.Value;

        public ITemplateSalaryRepository TemplateSalaryRepository => _templateSalaryRepository.Value;

        public IRepositoryBusinessEntity<BusinessEntity> BusinessEntityRepository => _businessEntity.Value;
    }
}

using Domain.Repositories.HR;
using Persistence.Repositories;
using Persistence.Repositories.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IUnitOfWorks> _unitOfWorks;
        private readonly Lazy<IJobTypeRepository> _jobTypeRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;

        public RepositoryManager(SmartDriveContext dbContext)
        {
            _unitOfWorks = new Lazy<IUnitOfWorks>(()=> new UnitOfWorks(dbContext));
            _jobTypeRepository = new Lazy<IJobTypeRepository> (()=> new JobTypeRepository(dbContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
        }

        public IJobTypeRepository JobTypeRepository => _jobTypeRepository.Value;

        public IUnitOfWorks UnitOfWorks => _unitOfWorks.Value;

        public IEmployeeRepository EmployeeRepository =>_employeeRepository.Value;
    }
}

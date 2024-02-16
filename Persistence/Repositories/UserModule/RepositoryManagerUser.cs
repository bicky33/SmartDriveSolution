using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.Repositories.UserModule;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.UserModule
{
    public class RepositoryManagerUser : IRepositoryManagerUser
    {
        private readonly Lazy<IUnitOfWorks> _unitOfWorks;
        private readonly Lazy<IRepositoryUser> _userRepository;
        private readonly Lazy<IRepositoryBusinessEntity<BusinessEntity>> _businessEntityRepo;
        private readonly Lazy<IRepositoryUserRole> _userRoleRepository;
        private readonly Lazy<IRepositoryUserPhone> _userPhoneRepository;
        private readonly Lazy<IRepositoryUserAddress> _userAddressRepository;

        public RepositoryManagerUser(SmartDriveContext dbContext)
        {
            _unitOfWorks = new Lazy<IUnitOfWorks>(() => new UnitOfWorks(dbContext));
            _userRepository = new Lazy<IRepositoryUser>(() =>
            new UserRepository(dbContext));
            _businessEntityRepo = new Lazy<IRepositoryBusinessEntity<BusinessEntity>>(() =>
            new BusinessEntityRepository(dbContext));
            _userRoleRepository = new Lazy<IRepositoryUserRole>(() =>
            new UserRoleRepository(dbContext));
            _userPhoneRepository = new Lazy<IRepositoryUserPhone>(() =>
            new UserPhoneRepository(dbContext));
            _userAddressRepository = new Lazy<IRepositoryUserAddress>(() =>
            new UserAddressRepository(dbContext));
        }

        public IRepositoryUser UserRepository => _userRepository.Value;

        public IUnitOfWorks UnitOfWork => _unitOfWorks.Value;

        public IRepositoryBusinessEntity<BusinessEntity> BusinessEntityRepository => _businessEntityRepo.Value;

        public IRepositoryUserRole UserRoleRepository => _userRoleRepository.Value;

        public IRepositoryUserPhone UserPhoneRepository => _userPhoneRepository.Value;

        public IRepositoryUserAddress UserAddressRepository => _userAddressRepository.Value;
    }
}

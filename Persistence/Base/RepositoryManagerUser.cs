using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.Repositories.UserModule;
using Persistence.Repositories;
using Persistence.Repositories.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Base
{
    public class RepositoryManagerUser : IRepositoryManagerUser
    {
        private readonly Lazy<IUnitOfWorks> _unitOfWorks;
        private readonly Lazy<IRepositoryEntityBase<User>> _userRepository;

        public RepositoryManagerUser(SmartDriveContext dbContext)
        {
            _unitOfWorks = new Lazy<IUnitOfWorks>(() => new UnitOfWorks(dbContext));
            _userRepository = new Lazy<IRepositoryEntityBase<User>>(() => 
            new UserRepository(dbContext));
        }

        public IRepositoryEntityBase<User> UserRepository => _userRepository.Value;

        public IUnitOfWorks UnitOfWork => _unitOfWorks.Value;
    }
}

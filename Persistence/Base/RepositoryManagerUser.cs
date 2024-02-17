using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.Repositories.UserModule;
using Persistence.Repositories;
using Persistence.Repositories.UserModule;

namespace Persistence.Base
{
    public class RepositoryManagerUser : IRepositoryManagerUser
    {
        private readonly Lazy<IUnitOfWorks> _unitOfWorks;
        private readonly Lazy<IRepositoryEntityBase<User>> _userRepository;
        private readonly Lazy<IRepositoryBusinessEntity<BusinessEntity>> _businessEntityRepo;

        public RepositoryManagerUser(SmartDriveContext dbContext)
        {
            _unitOfWorks = new Lazy<IUnitOfWorks>(() => new UnitOfWorks(dbContext));
            _userRepository = new Lazy<IRepositoryEntityBase<User>>(() => 
            new UserRepository(dbContext));
            _businessEntityRepo = new Lazy<IRepositoryBusinessEntity<BusinessEntity>>(() =>
            new BusinessEntityRepository(dbContext));
        }

        public IRepositoryEntityBase<User> UserRepository => _userRepository.Value;

        public IUnitOfWorks UnitOfWork => _unitOfWorks.Value;

        public IRepositoryBusinessEntity<BusinessEntity> BusinessEntityRepository => _businessEntityRepo.Value;

    }
}

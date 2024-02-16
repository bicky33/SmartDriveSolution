using Domain.Entities.Users;
using Domain.Repositories.Base;

namespace Domain.Repositories.UserModule
{
    public interface IRepositoryManagerUser
    {
        IRepositoryEntityBase<User> UserRepository { get; }
        IUnitOfWorks UnitOfWork { get; }
        IRepositoryBusinessEntity<BusinessEntity> BusinessEntityRepository { get; }
    }
}

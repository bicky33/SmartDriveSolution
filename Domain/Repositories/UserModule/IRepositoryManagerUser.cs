using Domain.Entities.Users;
using Domain.Repositories.Base;

namespace Domain.Repositories.UserModule
{
    public interface IRepositoryManagerUser
    {
        IRepositoryUser UserRepository { get; }
        IUnitOfWorks UnitOfWork { get; }
        IRepositoryBusinessEntity<BusinessEntity> BusinessEntityRepository { get; }
        IRepositoryUserRole UserRoleRepository { get; }
        IRepositoryUserPhone UserPhoneRepository { get; }
        IRepositoryUserAddress UserAddressRepository { get; }
        IRepositoryRole RoleRepository { get; }
    }
}

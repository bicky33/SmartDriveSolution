using Domain.Entities.Users;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

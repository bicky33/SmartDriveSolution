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
        IRepositoryEntityBase<User> UserRepository { get; }
        IUnitOfWorks UnitOfWork { get; }
        IRepositoryBusinessEntity<BusinessEntity> BusinessEntityRepository { get; }
    }
}

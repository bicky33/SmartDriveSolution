using Domain.Entities.Users;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.UserModule
{
    public interface IRepositoryUserAddress : IRepositoryEntityBase<UserAddress>
    {
        Task<IEnumerable<UserAddress>> GetAllEntityById(int id, bool trackChanges);
    }
}

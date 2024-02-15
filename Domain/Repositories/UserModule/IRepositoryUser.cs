using Domain.Entities.Users;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.UserModule
{
    public interface IRepositoryUser : IRepositoryEntityBase<User>
    {
        Task<User> GetUserByUsername(string username, bool trackChanges);
    }
}

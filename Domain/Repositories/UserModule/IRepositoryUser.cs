using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.RequestFeatured;
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
        Task<User> GetUserByEmail(string email, bool trackChanges);
        Task<User> GetUserByNationalId(string nationalId, bool trackChanges);
        Task<User> GetUserByNpwp(string npwp, bool trackChanges);
        Task<IEnumerable<User>> GetAllUsersPaging(EntityParameter entityParams, bool trackChanges);
    }
}

using Domain.Entities.Users;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.UserModule
{
    public interface IRepositoryUserPhone : IRepositoryEntityBase<UserPhone>
    {
        Task<IEnumerable<UserPhone>> GetAllEntityById(int id, bool trackChanges);
        Task<UserPhone> GetUserPhoneByIdAndPhone(int id, string phoneNumber, bool trackChanges);
        Task<UserPhone> GetByPhoneNumber(string phoneNumber, bool trackChanges);
    }
}

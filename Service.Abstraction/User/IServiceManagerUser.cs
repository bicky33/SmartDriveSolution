using Contract.DTO.UserModule;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.User
{
    public interface IServiceManagerUser
    {
        IServiceEntityBase<UserDto> UserService { get; }
    }
}

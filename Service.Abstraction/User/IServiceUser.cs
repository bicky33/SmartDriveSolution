using Contract.DTO.UserModule;
using Domain.Entities.Users;
using Domain.Enum;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.User
{
    public interface IServiceUser : IServiceEntityBase<UserDto>
    {
        Task UpdatePhoto(int id, UserEditProfileRequestDto entity);
        Task UpdatePassword(int id, UserUpdatePasswordRequestDto entity);
        Task UpdateEmail(int id, string newEmail);
        Task<UserDto> CreateUserWithRole(UserDto entity, string roleType, string isUserRoleActive);
    }
}

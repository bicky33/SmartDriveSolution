﻿using Contract.DTO.UserModule;
using Domain.Repositories.UserModule;
using Service.Abstraction.Base;
using Service.Abstraction.User;
using Service.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base.UserModule
{
    public class ServiceManagerUser : IServiceManagerUser
    {
        private readonly Lazy<IServiceEntityBase<UserDto>> _userService;

        public ServiceManagerUser(IRepositoryManagerUser repositoryUser)
        {
            _userService = new Lazy<IServiceEntityBase<UserDto>>(
                () => new UserService(repositoryUser));
        }

        public IServiceEntityBase<UserDto> UserService => _userService.Value;
    }
}

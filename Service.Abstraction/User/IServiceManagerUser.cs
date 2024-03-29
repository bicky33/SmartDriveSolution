﻿using Contract.DTO.UserModule;
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
        IServiceUser UserService { get; }
        IServiceBusinessEntity BusinessEntityService {  get; }
        IServiceUserRole UserRoleService {  get; }
        IServiceUserPhone UserPhoneService {  get; }
        IServiceUserAddress UserAddressService {  get; }
        IServiceLogin LoginService { get; }
        IServiceRole RoleService { get; }
    }
}

using Contract.DTO.UserModule;
using Domain.Authentication;
using Domain.Repositories.UserModule;
using Service.Abstraction.Base;
using Service.Abstraction.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserModule
{
    public class ServiceManagerUser : IServiceManagerUser
    {
        private readonly Lazy<IServiceUser> _userService;
        private readonly Lazy<IServiceBusinessEntity> _businessEntityService;
        private readonly Lazy<IServiceUserRole> _userRoleService;
        private readonly Lazy<IServiceUserPhone> _userPhoneService;
        private readonly Lazy<IServiceUserAddress> _userAddressService;
        private readonly Lazy<IServiceLogin> _loginService;
        private readonly Lazy<IServiceRole> _roleService;

        public ServiceManagerUser(IRepositoryManagerUser repositoryUser, 
            JwtTokenGenerator jwtGenerator)
        {
            _userService = new Lazy<IServiceUser>(
                () => new UserService(repositoryUser));
            _businessEntityService = new Lazy<IServiceBusinessEntity>(
                () => new BusinessEntityService(repositoryUser));
            _userRoleService = new Lazy<IServiceUserRole>(
                () => new UserRoleService(repositoryUser));
            _loginService = new Lazy<IServiceLogin>(
                () => new LoginService(repositoryUser, jwtGenerator));
            _userPhoneService = new Lazy<IServiceUserPhone>(
                () => new UserPhoneService(repositoryUser));
            _userAddressService = new Lazy<IServiceUserAddress>(
                () => new UserAddressService(repositoryUser));
            _roleService = new Lazy<IServiceRole>(
                () => new RoleService(repositoryUser));
        }

        public IServiceUser UserService => _userService.Value;

        public IServiceBusinessEntity BusinessEntityService => _businessEntityService.Value;

        public IServiceUserRole UserRoleService => _userRoleService.Value;

        public IServiceLogin LoginService => _loginService.Value;

        public IServiceUserPhone UserPhoneService => _userPhoneService.Value;

        public IServiceUserAddress UserAddressService => _userAddressService.Value;

        public IServiceRole RoleService => _roleService.Value;
    }
}

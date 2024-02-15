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
        private readonly Lazy<IServiceEntityBase<UserDto>> _userService;
        private readonly Lazy<IServiceBusinessEntity> _businessEntityService;
        private readonly Lazy<IServiceUserRole> _userRoleService;
        private readonly Lazy<IServiceLogin> _loginService;

        public ServiceManagerUser(IRepositoryManagerUser repositoryUser, JwtTokenGenerator jwtGenerator)
        {
            _userService = new Lazy<IServiceEntityBase<UserDto>>(
                () => new UserService(repositoryUser));
            _businessEntityService = new Lazy<IServiceBusinessEntity>(
                () => new BusinessEntityService(repositoryUser));
            _userRoleService = new Lazy<IServiceUserRole>(
                () => new UserRoleService(repositoryUser));
            _loginService = new Lazy<IServiceLogin>(
                () => new LoginService(repositoryUser, jwtGenerator));
        }

        public IServiceEntityBase<UserDto> UserService => _userService.Value;

        public IServiceBusinessEntity BusinessEntityService => _businessEntityService.Value;

        public IServiceUserRole UserRoleService => _userRoleService.Value;

        public IServiceLogin LoginService => _loginService.Value;
    }
}

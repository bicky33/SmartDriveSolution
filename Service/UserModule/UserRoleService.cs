using Contract.DTO.UserModule;
using Domain.Entities.Users;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories.UserModule;
using Mapster;
using Service.Abstraction.Base;
using Service.Abstraction.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserModule
{
    public class UserRoleService : IServiceUserRole
    {
        private readonly IRepositoryManagerUser _repositoryManager;

        public UserRoleService(IRepositoryManagerUser repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<UserRoleDto> CreateAsync(UserRoleDto entity)
        {
            var user = await _repositoryManager.UserRepository.GetEntityById(entity.UsroEntityid, false);
            if (user == null)
            {
                throw new EntityNotFoundException(entity.UsroEntityid, "User");
            }

            var userRole = entity.Adapt<UserRole>();

            //check if rolename is valid with enum types
            
            var checkRoleName = await _repositoryManager.RoleRepository.GetRole(entity.UsroRoleName, false);

            if (checkRoleName == null)
            {
                throw new EntityBadRequestException($"Invalid Role Name");
            }

            //check if user already have role entity tryna assign
            var userRoles = await _repositoryManager.UserRoleRepository.GetAllEntityById(user.UserEntityid, false);
            var containRoles = userRoles.Any(x => x.UsroRoleName == entity.UsroRoleName);

            if (containRoles)
            {
                throw new EntityBadRequestException($"User already has {entity.UsroRoleName} role");
            }

            userRole.UsroModifiedDate = DateTime.Now;
            
            _repositoryManager.UserRoleRepository.CreateEntity(userRole);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return userRole.Adapt<UserRoleDto>();
        }

        public async Task DeleteAsync(int id, string roleName)
        {
            //var userRole = await _repositoryManager.UserRoleRepository.GetEntityById(id, false);

            //check if rolename is valid with enum types

            var checkRoleName = await _repositoryManager.RoleRepository.GetRole(roleName, false);

            if (checkRoleName == null)
            {
                throw new EntityBadRequestException($"Invalid Role Name");
            }

            var userRole = await _repositoryManager.UserRoleRepository.GetSingleUserRoleByIdAndUserRole(id, roleName, false);
            if (userRole == null)
            {
                throw new EntityNotFoundException(id, "User Role");
            }
            _repositoryManager.UserRoleRepository.DeleteEntity(userRole);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserRoleDto>> GetAllAsync(bool trackChanges)
        {
            var userRoles = await _repositoryManager.UserRoleRepository.GetAllEntity(false);
            var userRolesDto = userRoles.Adapt<IEnumerable<UserRoleDto>>();

            return userRolesDto;
        }

        public async Task<IEnumerable<UserRoleDto>> GetAllByIdAsync(int id, bool trackChanges)
        {
            var userRoles = await _repositoryManager.UserRoleRepository.GetAllEntityById(id, false);

            if (userRoles == null)
            {
                throw new EntityNotFoundException(id, "User Role");
            }

            var userRolesDto = userRoles.Adapt<IEnumerable<UserRoleDto>>();

            return userRolesDto;
        }

        public async Task<UserRoleDto> GetByIdAsync(int id, bool trackChanges)
        {
            var userRole = await _repositoryManager.UserRoleRepository.GetEntityById(id, false);

            if (userRole == null)
            {
                throw new EntityNotFoundException(id, "User Role");
            }

            var userRoleDto = userRole.Adapt<UserRoleDto>();

            return userRoleDto;
        }
    }
}

using Contract.DTO.UserModule;
using Domain.Entities.Users;
using Domain.Exceptions;
using Domain.Repositories.UserModule;
using Mapster;
using Service.Abstraction.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserModule
{
    public class RoleService : IServiceRole
    {
        private readonly IRepositoryManagerUser _repositoryManager;

        public RoleService(IRepositoryManagerUser repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<RoleDto> CreateAsync(RoleDto entity)
        {
            if(entity.RoleName.Length > 2)
            {
                throw new EntityBadRequestException($"Max role name length is 2");
            }

            var checkRole = await _repositoryManager.RoleRepository.GetRole(entity.RoleName, false);

            if (checkRole != null)
            {
                throw new EntityBadRequestException($"{entity.RoleName} already exist");
            }

            var role = entity.Adapt<Role>();

            _repositoryManager.RoleRepository.CreateEntity(role);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return role.Adapt<RoleDto>();
        }

        public async Task DeleteAsync(string roleName)
        {
            var role = await _repositoryManager.RoleRepository.GetRole(roleName, false);
            if (role == null)
            {
                throw new EntityNotFoundException(roleName, "Role");
            }

            _repositoryManager.RoleRepository.DeleteEntity(role);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync(bool trackChanges)
        {
            var role = await _repositoryManager.RoleRepository.GetAllEntity(false);

            var roleDto = role.Adapt<IEnumerable<RoleDto>>();

            return roleDto;
        }

        public async Task<RoleDto> GetByRoleNameAsync(string roleName, bool trackChanges)
        {
            var role = await _repositoryManager.RoleRepository.GetRole(roleName, false);

            if (role == null)
            {
                throw new EntityNotFoundException(roleName, "Role");
            }

            var roleDto = role.Adapt<RoleDto>();

            return roleDto;
        }

        public async Task UpdateAsync(string roleName, RoleDto entity)
        {
            if (entity.RoleName.Length > 2)
            {
                throw new EntityBadRequestException($"Max role name length is 2");
            }

            if (roleName != entity.RoleName)
            {
                throw new EntityBadRequestException($"Only description that can be change");
            }

            var role = await _repositoryManager.RoleRepository.GetRole(roleName, true);

            if (role == null)
            {
                throw new EntityNotFoundException(roleName, "Role");
            }

            var checkRole = await _repositoryManager.RoleRepository.GetRole(entity.RoleName, false);

            if (checkRole != null && role.RoleName != checkRole.RoleName)
            {
                throw new EntityBadRequestException($"{entity.RoleName} already exist");
            }

            role.RoleName = entity.RoleName;
            role.RoleDescription = entity.RoleDescription;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }
    }
}

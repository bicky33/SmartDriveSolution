using Contract.DTO.UserModule;
using Domain.Entities.Users;
using Domain.Exceptions;
using Domain.Repositories.UserModule;
using Mapster;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserModule
{
    public class UserService : IServiceEntityBase<UserDto>
    {
        private readonly IRepositoryManagerUser _repositoryManager;

        public UserService(IRepositoryManagerUser repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<UserDto> CreateAsync(UserDto entity)
        {
            entity.UserPassword = BCrypt.Net.BCrypt.HashPassword(entity.UserPassword);
            var user = entity.Adapt<User>();

            _repositoryManager.UserRepository.CreateEntity(user);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return user.Adapt<UserDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _repositoryManager.UserRepository.GetEntityById(id, false);
            if(user == null)
            {
                throw new EntityNotFoundException(id);
            }

            _repositoryManager.UserRepository.DeleteEntity(user);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync(bool trackChanges)
        {
            var users = await _repositoryManager.UserRepository.GetAllEntity(trackChanges);
            var usersDto = users.Adapt<IEnumerable<UserDto>>();

            return usersDto;
        }

        public async Task<UserDto> GetByIdAsync(int id, bool trackChanges)
        {
            var user = await _repositoryManager.UserRepository.GetEntityById(id, trackChanges);
            if (user == null)
            {
                throw new EntityNotFoundException(id);
            }

            var userDto = user.Adapt<UserDto>();
            return userDto;
        }

        public async Task UpdateAsync(int id, UserDto entity)
        {
            var user = await _repositoryManager.UserRepository.GetEntityById(id, true);
            if (user == null)
            {
                throw new EntityNotFoundException(id);
            }

            user.UserName = entity.UserName;
            user.UserPassword = entity.UserPassword;
            user.UserFullName = entity.UserFullName;
            user.UserEmail = entity.UserEmail;
            user.UserBirthPlace = entity.UserBirthPlace;
            user.UserBirthDate = entity.UserBirthDate;
            user.UserNationalId = entity.UserNationalId;
            user.UserNpwp = entity.UserNpwp;
            user.UserPhoto = entity.UserPhoto;
            user.UserModifiedDate = DateTime.Now;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }
    }
}

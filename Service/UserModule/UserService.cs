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
            var checkUsername = await _repositoryManager.UserRepository.GetUserByUsername(entity.UserName, false);

            if(checkUsername != null) {
                throw new EntityBadRequestException("username already exist");
            }

            //businessentity
            var businessEntity = _repositoryManager.BusinessEntityRepository.CreateEntity();
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            //var user = entity.Adapt<User>();

            ////assign businessentity
            //user.UserEntityid = businessEntity.Entityid;

            ////hash password
            //user.UserPassword = BCrypt.Net.BCrypt.HashPassword(user.UserPassword);

            ////modifieddate
            //user.UserModifiedDate = DateTime.Now;

            ////set null userroles
            //user.UserRoles = null;

            var user = new User()
            {
                UserEntityid = businessEntity.Entityid,
                UserName = entity.UserName,
                UserPassword = BCrypt.Net.BCrypt.HashPassword(entity.UserPassword),
                UserFullName = entity.UserFullName,
                UserEmail = entity.UserEmail,
                UserBirthPlace = entity.UserBirthPlace,
                UserBirthDate = entity.UserBirthDate,
                UserNationalId = entity.UserNationalId,
                UserNpwp = entity.UserNpwp,
                UserModifiedDate = DateTime.Now,
                UserRoles = null
            };

            _repositoryManager.UserRepository.CreateEntity(user);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            //return user.Adapt<UserDto>();

            //var mapsterConfig = TypeAdapterConfig<User, UserDto>.NewConfig().Ignore(dest => dest.UserPassword);
            var mapsterConfig = TypeAdapterConfig.GlobalSettings.Clone();
            mapsterConfig.Default.Ignore("UserPassword");
            return user.Adapt<UserDto>(mapsterConfig);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _repositoryManager.UserRepository.GetEntityById(id, false);
            if(user == null)
            {
                throw new EntityNotFoundException(id, "user");
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
                throw new EntityNotFoundException(id, "user");
            }

            var userDto = user.Adapt<UserDto>();
            return userDto;
        }

        public async Task UpdateAsync(int id, UserDto entity)
        {
            var user = await _repositoryManager.UserRepository.GetEntityById(id, true);
            if (user == null)
            {
                throw new EntityNotFoundException(id, "user");
            }

            var checkUsername = await _repositoryManager.UserRepository.GetUserByUsername(entity.UserName, false);

            if (checkUsername != null && user.UserName != checkUsername.UserName)
            {
                throw new EntityBadRequestException("username already exist");
            }

            user.UserName = entity.UserName;
            user.UserPassword = BCrypt.Net.BCrypt.HashPassword(entity.UserPassword);
            user.UserFullName = entity.UserFullName;
            user.UserEmail = entity.UserEmail;
            user.UserBirthPlace = entity.UserBirthPlace;
            user.UserBirthDate = entity.UserBirthDate;
            user.UserNationalId = entity.UserNationalId;
            user.UserNpwp = entity.UserNpwp;
            user.UserPhoto = entity.UserPhoto;
            user.UserModifiedDate = DateTime.Now;
            //user.UserRoles = null;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }
    }
}

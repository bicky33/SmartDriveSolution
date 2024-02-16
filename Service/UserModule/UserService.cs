using Contract.DTO.UserModule;
using Domain.Entities.Users;
using Domain.Exceptions;
using Domain.Repositories.UserModule;
using Mapster;
using Service.Abstraction.Base;
using Service.Abstraction.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserModule
{
    public class UserService : IServiceUser
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

        public async Task UpdatePhoto(int id, UserEditProfileRequestDto entity)
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
            // save photo
            if(entity.UserPhoto != null)
            {
                var file = entity.UserPhoto;
                var folderName = Path.Combine("Resources", "Images", "Users");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = user.UserEntityid.ToString() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    //user.UserPhoto = fileName;
                    //user.UserName = entity.UserName;
                    //user.UserFullName = entity.UserFullName;
                    //user.UserBirthPlace = entity.UserBirthPlace;
                    //user.UserBirthDate = entity.UserBirthDate;

                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        user.UserPhoto = fileName;
                    }
                }
            }


            if (!string.IsNullOrWhiteSpace(entity.UserName))
            {
                user.UserName = entity.UserName;
            }

            if (!string.IsNullOrWhiteSpace(entity.UserFullName))
            {
                user.UserFullName = entity.UserFullName;
            }

            if (!string.IsNullOrWhiteSpace(entity.UserBirthPlace))
            {
                user.UserBirthPlace = entity.UserBirthPlace;
            }

            if (entity.UserBirthDate != null)
            {
                user.UserBirthDate = entity.UserBirthDate;
            }

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }
    }
}

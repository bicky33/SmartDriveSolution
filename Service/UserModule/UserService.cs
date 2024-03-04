using Contract.DTO.UserModule;
using Domain.Entities.Users;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories.UserModule;
using Domain.RequestFeatured;
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
                UserRoles = null,
                UserPhones = null,
                UserAddresses = null,
            };

            _repositoryManager.UserRepository.CreateEntity(user);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            var mapsterConfig = TypeAdapterConfig.GlobalSettings.Clone();
            mapsterConfig.Default.Ignore("UserPassword");
            return user.Adapt<UserDto>(mapsterConfig);
        }

        public async Task<UserDto> CreateUserWithRole(UserDto entity, string roleType, string isUserRoleActive)
        {
            var checkUsername = await _repositoryManager.UserRepository.GetUserByUsername(entity.UserName, false);

            if (checkUsername != null)
            {
                throw new EntityBadRequestException("username already exist");
            }

            var checkUserEmail = await _repositoryManager.UserRepository.GetUserByEmail(entity.UserEmail, false);

            if (checkUserEmail != null)
            {
                throw new EntityBadRequestException("email already exist");
            }

            var checkNationalId = await _repositoryManager.UserRepository.GetUserByNationalId(entity.UserNationalId, false);

            if (checkNationalId != null)
            {
                throw new EntityBadRequestException("National ID already exist");
            }

            var checkNpwp = await _repositoryManager.UserRepository.GetUserByNpwp(entity.UserNpwp, false);

            if (checkNpwp != null)
            {
                throw new EntityBadRequestException("National ID already exist");
            }


            //check if rolename is valid with enum types

            var checkRoleName = await _repositoryManager.RoleRepository.GetRole(roleType, false);

            if (checkRoleName == null)
            {
                throw new EntityBadRequestException($"Invalid Role Name");
            }

            //check if role status is valid with enum types
            bool isRoleStatusValid = typeof(EnumRoleActiveStatus)
           .GetFields()
           .Any(f => f.GetValue(null).ToString() == isUserRoleActive);

            if (!isRoleStatusValid)
            {
                throw new EntityBadRequestException($"Invalid Role Name");
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
                UserRoles = null,
                UserPhones = null,
                UserAddresses = null,
            };

            _repositoryManager.UserRepository.CreateEntity(user);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            //assign role
            var entityRole = new UserRole()
            {
                UsroEntityid = businessEntity.Entityid,
                UsroModifiedDate = DateTime.Now,
                UsroRoleName = roleType,
                UsroStatus = isUserRoleActive
            };

            _repositoryManager.UserRoleRepository.CreateEntity(entityRole);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            var mapsterConfig = TypeAdapterConfig.GlobalSettings.Clone();
            mapsterConfig.Default.Ignore("UserPassword");

            var userDto = user.Adapt<UserDto>(mapsterConfig);


            return userDto;
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

            var mapsterConfig = TypeAdapterConfig.GlobalSettings.Clone();
            mapsterConfig.Default.Ignore("UserPassword");
            return users.Adapt<IEnumerable<UserDto>>(mapsterConfig);
        }

        public async Task<UserDto> GetByIdAsync(int id, bool trackChanges)
        {
            var user = await _repositoryManager.UserRepository.GetEntityById(id, trackChanges);
            if (user == null)
            {
                throw new EntityNotFoundException(id, "user");
            }

            var mapsterConfig = TypeAdapterConfig.GlobalSettings.Clone();
            mapsterConfig.Default.Ignore("UserPassword");

            return user.Adapt <UserDto> (mapsterConfig);
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

        public async Task UpdateEmail(int id, string newEmail)
        {
            var user = await _repositoryManager.UserRepository.GetEntityById(id, true);
            if (user == null)
            {
                throw new EntityNotFoundException(id, "user");
            }

            var checkUserEmail = await _repositoryManager.UserRepository.GetUserByEmail(newEmail, false);

            if (checkUserEmail != null && user.UserEmail != checkUserEmail.UserEmail)
            {
                throw new EntityBadRequestException("email already exist");
            }

            user.UserEmail = newEmail;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePassword(int id, UserUpdatePasswordRequestDto entity)
        {
            var user = await _repositoryManager.UserRepository.GetEntityById(id, true);

            if (user == null)
            {
                throw new EntityNotFoundException(id, "user");
            }

            if (BCrypt.Net.BCrypt.Verify(entity.CurrentPassword, user.UserPassword) == false)
            {
                throw new EntityBadRequestException("Incorrect Password");
            }

            if(entity.NewPassword != entity.ConfirmNewPassword)
            {
                throw new EntityBadRequestException("The new password and confirm new password do not match");
            }

            user.UserPassword = BCrypt.Net.BCrypt.HashPassword(entity.NewPassword);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePhoto(int id, UserEditProfileRequestDto entity)
        {
            var user = await _repositoryManager.UserRepository.GetEntityById(id, true);
            if (user == null)
            {
                throw new EntityNotFoundException(id, "user");
            }


            if (!string.IsNullOrWhiteSpace(entity.UserName))
            {
                user.UserName = entity.UserName;

                var checkUsername = await _repositoryManager.UserRepository.GetUserByUsername(entity.UserName, false);

                if (checkUsername != null && user.UserName != checkUsername.UserName)
                {
                    throw new EntityBadRequestException("username already exist");
                }
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
            // save photo
            if (entity.UserPhoto != null)
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

                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        user.UserPhoto = fileName;
                    }
                }
            }

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetUsersPaging(EntityParameter entityParameter, bool trackChanges)
        {
            var users = await _repositoryManager.UserRepository.GetAllUsersPaging(entityParameter, trackChanges);

            var mapsterConfig = TypeAdapterConfig.GlobalSettings.Clone();
            mapsterConfig.Default.Ignore("UserPassword");

            return users.Adapt<IEnumerable<UserDto>>(mapsterConfig);
        }

        public async Task ForgotPassword(UserForgotPasswordDto body)
        {
            var username = await _repositoryManager.UserRepository.GetUserByUsername(body.UserName, true);

            if(username == null)
            {
                throw new EntityNotFoundException("Username", "User");
            }

            if(username.UserBirthPlace != body.UserBirthPlace)
            {
                throw new EntityNotFoundException("Birthplace", "User");
            }

            if(body.UserNationalId != username.UserNationalId)
            {
                throw new EntityNotFoundException("National ID", "User");
            }

            if (body.UserNpwp != username.UserNpwp)
            {
                throw new EntityNotFoundException("NPWP", "User");
            }

            if (body.NewPassword != body.ConfirmNewPassword)
            {
                throw new EntityBadRequestException("The new password and confirm new password do not match");
            }

            username.UserPassword = BCrypt.Net.BCrypt.HashPassword(body.NewPassword);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }
    }
}

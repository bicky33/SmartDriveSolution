using Contract.DTO.UserModule;
using Domain.Entities.Users;
using Domain.Enum;
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
    public class UserPhoneService : IServiceUserPhone
    {
        private readonly IRepositoryManagerUser _repositoryManager;

        public UserPhoneService(IRepositoryManagerUser repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<UserPhoneDto> CreateAsync(UserPhoneDto entity)
        {
            var user = await _repositoryManager.UserRepository.GetEntityById(entity.UsphEntityid, false);
            if (user == null)
            {
                throw new EntityNotFoundException(entity.UsphEntityid, "User");
            }

            var checkPhoneNumber = await _repositoryManager.UserPhoneRepository.GetByPhoneNumber(entity.UsphPhoneNumber, false);

            if (checkPhoneNumber != null)
            {
                throw new EntityBadRequestException("phone number already exist");
            }

            var userPhone = entity.Adapt<UserPhone>();

            //check if phone type is valid
            bool isPhoneType = typeof(UserPhoneType)
           .GetFields()
           .Any(f => f.GetValue(null).ToString() == entity.UsphPhoneType);

            if (!isPhoneType)
            {
                throw new EntityBadRequestException($"Invalid Phone Type");
            }

            //check if user already have phone number
            var currentPhones = await _repositoryManager.UserPhoneRepository.GetAllEntityById(user.UserEntityid, false);
            var containPhones = currentPhones.Any(x => x.UsphPhoneNumber == entity.UsphPhoneNumber);

            if (containPhones)
            {
                throw new EntityBadRequestException($"User already has this phone number");
            }

            userPhone.UsphModifiedDate = DateTime.Now;

            _repositoryManager.UserPhoneRepository.CreateEntity(userPhone);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return userPhone.Adapt<UserPhoneDto>();
        }

        public async Task DeleteAsync(int id, string userPhoneNumber)
        {
            var userPhone = await _repositoryManager.UserPhoneRepository.GetUserPhoneByIdAndPhone(id, userPhoneNumber, false);

            if (userPhone == null)
            {
                throw new EntityNotFoundException(id, "User Phone");
            }

            _repositoryManager.UserPhoneRepository.DeleteEntity(userPhone);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserPhoneDto>> GetAllAsync(bool trackChanges)
        {
            var userPhones = await _repositoryManager.UserPhoneRepository.GetAllEntity(false);
            var userPhonesDto = userPhones.Adapt<IEnumerable<UserPhoneDto>>();

            return userPhonesDto;
        }

        public async Task<IEnumerable<UserPhoneDto>> GetAllByIdAsync(int id, bool trackChanges)
        {
            var userPhones = await _repositoryManager.UserPhoneRepository.GetAllEntityById(id, false);

            if (userPhones == null)
            {
                throw new EntityNotFoundException(id, "User Phone");
            }

            var userPhonesDto = userPhones.Adapt<IEnumerable<UserPhoneDto>>();

            return userPhonesDto;
        }

        public async Task<UserPhoneDto> GetByIdAndPhoneNumberAsync(int id, string userPhoneNumber, bool trackChanges)
        {
            var userPhone = await _repositoryManager.UserPhoneRepository.GetUserPhoneByIdAndPhone(id, userPhoneNumber, false);

            if (userPhone == null)
            {
                throw new EntityNotFoundException(id, "User Phone");
            }

            var userPhoneDto = userPhone.Adapt<UserPhoneDto>();

            return userPhoneDto;
        }

        public async Task<UserPhoneDto> UpdateAsync(int id, string phoneNumber, UserPhoneDto entity)
        {
            var userPhone = await _repositoryManager.UserPhoneRepository.GetUserPhoneByIdAndPhone(id, phoneNumber, true);

            if (userPhone == null)
            {
                throw new EntityNotFoundException(id, "User Phone");
            }

            var checkPhoneNumber = await _repositoryManager.UserPhoneRepository.GetByPhoneNumber(entity.UsphPhoneNumber, false);

            if (checkPhoneNumber != null && userPhone.UsphPhoneNumber != checkPhoneNumber.UsphPhoneNumber)
            {
                throw new EntityBadRequestException("phone number already exist");
            }

            //check if phone type is valid
            bool isPhoneType = typeof(UserPhoneType)
           .GetFields()
           .Any(f => f.GetValue(null).ToString() == entity.UsphPhoneType);

            if (!isPhoneType)
            {
                throw new EntityBadRequestException($"Invalid Phone Type");
            }

            //userPhone.UsphPhoneNumber = entity.UsphPhoneNumber;
            //userPhone.UsphPhoneType = entity.UsphPhoneType;
            //userPhone.UsphStatus = entity.UsphStatus;
            //userPhone.UsphMime = entity.UsphMime;
            //userPhone.UsphModifiedDate = DateTime.Now;
            //await _repositoryManager.UnitOfWork.SaveChangesAsync();

            //delete current phoneNumber
            _repositoryManager.UserPhoneRepository.DeleteEntity(userPhone);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            //create new phoneNumber
            var newUserPhone = entity.Adapt<UserPhone>();
            newUserPhone.UsphModifiedDate = DateTime.Now;

            _repositoryManager.UserPhoneRepository.CreateEntity(newUserPhone);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return newUserPhone.Adapt<UserPhoneDto>();
        }
    }
}

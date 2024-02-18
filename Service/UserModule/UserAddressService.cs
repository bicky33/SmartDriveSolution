using Contract.DTO.UserModule;
using Domain.Entities.Master;
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
    public class UserAddressService : IServiceUserAddress
    {
        private readonly IRepositoryManagerUser _repositoryManager;

        public UserAddressService(IRepositoryManagerUser repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<UserAddressDto> CreateAsync(UserAddressDto entity)
        {
            var address = entity.Adapt<UserAddress>();
            address.UsdrModifiedDate = DateTime.Now;

            _repositoryManager.UserAddressRepository.CreateEntity(address);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return address.Adapt<UserAddressDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var address = await _repositoryManager.UserAddressRepository.GetEntityById(id, false);
            if (address == null)
            {
                throw new EntityNotFoundException(id, "User Address");
            }

            _repositoryManager.UserAddressRepository.DeleteEntity(address);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserAddressDto>> GetAllAsync(bool trackChanges)
        {
            var userAddresses = await _repositoryManager.UserAddressRepository.GetAllEntity(false);
            var userAddressesDto = userAddresses.Adapt<IEnumerable<UserAddressDto>>();

            return userAddressesDto;
        }

        public async Task<IEnumerable<UserAddressDto>> GetAllByIdAsync(int id, bool trackChanges)
        {
            var userAddress = await _repositoryManager.UserAddressRepository.GetAllEntityById(id, false);

            if (userAddress == null)
            {
                throw new EntityNotFoundException(id, "User Address");
            }

            var userAddressDto = userAddress.Adapt<IEnumerable<UserAddressDto>>();

            return userAddressDto;
        }

        public async Task<UserAddressDto> GetByIdAsync(int id, bool trackChanges)
        {
            var address = await _repositoryManager.UserAddressRepository.GetEntityById(id, false);

            if (address == null)
            {
                throw new EntityNotFoundException(id, "User Address");
            }

            var addressdto = address.Adapt<UserAddressDto>();

            return addressdto;
        }

        public async Task UpdateAsync(int id, UserAddressDto entity)
        {
            var address = await _repositoryManager.UserAddressRepository.GetEntityById(id, true);

            if (address == null)
            {
                throw new EntityNotFoundException(id, "User Address");
            }

            address.UsdrAddress1 = entity.UsdrAddress1;
            address.UsdrAddress2 = entity.UsdrAddress2;

            address.UsdrModifiedDate = DateTime.Now;
            address.UsdrCityId = entity.UsdrCityId;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }
    }
}

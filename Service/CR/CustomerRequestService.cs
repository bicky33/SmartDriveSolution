using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Domain.Entities.Users;
using Domain.Exceptions;
using Domain.Repositories.CR;
using Domain.Repositories.UserModule;
using Mapster;
using Microsoft.AspNetCore.Components.Forms;
using Service.Abstraction.CR;
using Service.Abstraction.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CR
{
    public class CustomerRequestService : ICustomerRequestService
    {
        private readonly IRepositoryCustomerManager _repositoryCustomerManager;
        private readonly IRepositoryManagerUser _repositoryManagerUser;
        //private readonly IServiceCustomerManager _serviceCustomerManager;

        public CustomerRequestService(
            IRepositoryCustomerManager repositoryCustomerManager,
            IRepositoryManagerUser repositoryManagerUser)
        {
            _repositoryCustomerManager = repositoryCustomerManager;
            _repositoryManagerUser = repositoryManagerUser;
            //_serviceCustomerManager = serviceCustomerManager;
        }

        public async Task<CustomerRequestDto> CreateAsync(CustomerRequestDto entity)
        {
            var businessEntity = _repositoryManagerUser.BusinessEntityRepository.CreateEntity();
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

            var customerRequest = entity.Adapt<CustomerRequest>();
            customerRequest.CreqEntityid = businessEntity.Entityid;
            _repositoryCustomerManager.CustomerRequestRepository.CreateEntity(customerRequest);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

            return customerRequest.Adapt<CustomerRequestDto>();
        }

        public async Task<CustomerRequestDto> CreateCustomerRequest(CustomerRequestDto entity)
        {
            var businessEntity = _repositoryManagerUser.BusinessEntityRepository.CreateEntity();
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

            // Create new CustomerInscAsset
            var customerInscAsset = entity.CustomerInscAsset.Adapt<CustomerInscAsset>();
            var newCustomerInscAsset = _repositoryCustomerManager.CustomerInscAssetRepository.CreateData(customerInscAsset);

            // Create new CustomerClaim
            var customerClaim = new CustomerClaimDto();
            var newCustomerClaim = _repositoryCustomerManager.CustomerClaimRepository.CreateData(customerClaim.Adapt<CustomerClaim>());

            //var customerRequest = entity.Adapt<CustomerRequest>();
            var customerRequest = new CustomerRequest()
            {
                CreqEntityid = businessEntity.Entityid,
                CreqCreateDate = DateTime.Now,
                CreqStatus = entity.CreqStatus,
                CreqType = entity.CreqType,
                CreqCustEntityid = entity.CreqCustEntityid,
                CreqAgenEntityid = entity.CreqAgenEntityid,
                CustomerInscAsset = newCustomerInscAsset,
                CustomerClaim = newCustomerClaim
            };
            //customerRequest.CreqEntityid = businessEntity.Entityid;
            //customerRequest.CustomerClaim = newCustomerClaim;
            //customerRequest.CustomerInscAsset = newCustomerInscAsset;

            _repositoryCustomerManager.CustomerRequestRepository.CreateEntity(customerRequest);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

            return customerRequest.Adapt<CustomerRequestDto>();
        }

        //public async Task<CustomerRequestDto> CreateByAgen(CustomerRequestDto entity)
        //{
        //    var newBusinessEntity = await _serviceManagerUser.BusinessEntityService.CreateBusinessEntity();
        //    var newInscAsset = await _serviceCustomerManager.CustomerInscAssetService.CreateAsync(entity.CustomerInscAsset);

        //    var customerRequest = entity.Adapt<CustomerRequest>();
        //    customerRequest.CreqEntityid = newBusinessEntity.Entityid;
        //    _repositoryCustomerManager.CustomerRequestRepository.CreateEntity(customerRequest);
        //    await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

        //    return customerRequest.Adapt<CustomerRequestDto>();
        //}

        public async Task DeleteAsync(int id)
        {
            var customerRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetEntityById(id, false);
            if (customerRequest == null)
            {
                throw new EntityNotFoundException(id, "CustomerRequest");
            }

            _repositoryCustomerManager.CustomerRequestRepository.DeleteEntity(customerRequest);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerRequestDto>> GetAllAsync(bool trackChanges)
        {
            var customerRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetAllEntity(false);
            var customerRequestDto = customerRequest.Adapt<IEnumerable<CustomerRequestDto>>();
            
            return customerRequestDto;
        }

        public async Task<IEnumerable<CustomerRequestDto>> GetAllByEmployee(string eawgCode, bool trackChanges)
        {
            var customerRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetAllByEmployee(eawgCode, false);
            var customerRequestDto = customerRequest.Adapt<IEnumerable<CustomerRequestDto>>();

            return customerRequestDto;
        }

        public async Task<IEnumerable<CustomerRequestDto>> GetAllByUser(int userId, bool trackChanges)
        {
            var customerRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetAllByUserId(userId, false);
            var customerRequestDto = customerRequest.Adapt<IEnumerable<CustomerRequestDto>>();

            return customerRequestDto;
        }

        public async Task<CustomerRequestDto> GetByIdAsync(int id, bool trackChanges)
        {
            var customerRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetEntityById(id, false);
            if(customerRequest == null)
            {
                throw new EntityNotFoundException(id, "CustomerRequest");
            }
            var customerRequestDto = customerRequest.Adapt<CustomerRequestDto>();
            return customerRequestDto;
        }

        public async Task UpdateAsync(int id, CustomerRequestDto entity)
        {
            var customerRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetEntityById(id, true);
            if(customerRequest == null)
            {
                throw new EntityNotFoundException(id, "CustomerRequest");
            }
            customerRequest.CreqStatus = entity.CreqStatus;
            customerRequest.CreqType = entity.CreqType;
            customerRequest.CreqModifiedDate = DateTime.Now;
            customerRequest.CreqCustEntityid = entity.CreqCustEntityid;
            customerRequest.CreqAgenEntityid = entity.CreqAgenEntityid;

            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
        }
    }
}

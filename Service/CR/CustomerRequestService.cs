using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Domain.Exceptions;
using Domain.Repositories.CR;
using Domain.Repositories.UserModule;
using Mapster;
using Service.Abstraction.CR;
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

        public CustomerRequestService(IRepositoryCustomerManager repositoryCustomerManager, IRepositoryManagerUser repositoryManagerUser)
        {
            _repositoryCustomerManager = repositoryCustomerManager;
            _repositoryManagerUser = repositoryManagerUser;
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

            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
        }
    }
}

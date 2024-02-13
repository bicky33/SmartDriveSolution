using Contract.DTO.CR.Request;
using Domain.Entities.CR;
using Domain.Exceptions;
using Domain.Repositories.CR;
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
        private readonly ICustomerRepositoryManager _customerRepositoryManager;

        public CustomerRequestService(ICustomerRepositoryManager customerRepositoryManager)
        {
            _customerRepositoryManager = customerRepositoryManager;
        }

        public async Task<CustomerRequestDto> CreateAsync(CustomerRequestDto entity)
        {
            var customerRequest = entity.Adapt<CustomerRequest>();
            _customerRepositoryManager.CustomerRequestRepository.CreateEntity(customerRequest);
            await _customerRepositoryManager.CustomerUnitOfWork.SaveChangesAsync();

            return customerRequest.Adapt<CustomerRequestDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var customerRequest = await _customerRepositoryManager.CustomerRequestRepository.GetEntityById(id, false);
            if (customerRequest == null)
            {
                throw new EntityNotFoundException(id);
            }

            _customerRepositoryManager.CustomerRequestRepository.DeleteEntity(customerRequest);
            await _customerRepositoryManager.CustomerUnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerRequestDto>> GetAllAsync(bool trackChanges)
        {
            var customerRequest = await _customerRepositoryManager.CustomerRequestRepository.GetAllEntity(false);
            var customerRequestDto = customerRequest.Adapt<IEnumerable<CustomerRequestDto>>();
            return customerRequestDto;
        }

        public async Task<CustomerRequestDto> GetByIdAsync(int id, bool trackChanges)
        {
            var customerRequest = await _customerRepositoryManager.CustomerRequestRepository.GetEntityById(id, false);
            if(customerRequest == null)
            {
                throw new EntityNotFoundException(id);
            }
            var customerRequestDto = customerRequest.Adapt<CustomerRequestDto>();
            return customerRequestDto;
        }

        public async Task<CustomerRequestDto> UpdateAsync(int id, CustomerRequestDto entity)
        {
            var customerRequest = await _customerRepositoryManager.CustomerRequestRepository.GetEntityById(id, false);
            if(customerRequest == null)
            {
                throw new EntityNotFoundException(id);
            }
            customerRequest.CreqStatus = entity.CreqStatus;
            customerRequest.CreqType = entity.CreqType;
            customerRequest.CreqModifiedDate = DateTime.Now;

            await _customerRepositoryManager.CustomerUnitOfWork.SaveChangesAsync();

            return customerRequest.Adapt<CustomerRequestDto>();

        }
    }
}

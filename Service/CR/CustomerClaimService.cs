using Contract.DTO.CR.Response;
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
    public class CustomerClaimService : ICustomerClaimService
    {
        private readonly IRepositoryCustomerManager _repositoryCustomerManager;

        public CustomerClaimService(IRepositoryCustomerManager repositoryCustomerManager)
        {
            _repositoryCustomerManager = repositoryCustomerManager;
        }

        public async Task<CustomerClaimDto> CreateAsync(CustomerClaimDto entity)
        {
            var customerClaim = entity.Adapt<CustomerClaim>();
            _repositoryCustomerManager.CustomerClaimRepository.CreateEntity(customerClaim);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

            return customerClaim.Adapt<CustomerClaimDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var customerClaim = await _repositoryCustomerManager.CustomerClaimRepository.GetEntityById(id, false);
            if (customerClaim == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscAsset");
            }

            _repositoryCustomerManager.CustomerClaimRepository.DeleteEntity(customerClaim);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerClaimDto>> GetAllAsync(bool trackChanges)
        {
            var customerClaim = await _repositoryCustomerManager.CustomerClaimRepository.GetAllEntity(false);
            var customerClaimDto = customerClaim.Adapt<IEnumerable<CustomerClaimDto>>();

            return customerClaimDto;
        }

        public async Task<CustomerClaimDto> GetByIdAsync(int id, bool trackChanges)
        {
            var customerClaim = await _repositoryCustomerManager.CustomerClaimRepository.GetEntityById(id, false);
            if(customerClaim == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscAsset");
            }

            var customerClaimDto = customerClaim.Adapt<CustomerClaimDto>();
            return customerClaimDto;
        }

        public async Task UpdateAsync(int id, CustomerClaimDto entity)
        {
            var customerClaim = await _repositoryCustomerManager.CustomerClaimRepository.GetEntityById(id, true);
            if (customerClaim == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscAsset");
            }

            customerClaim.CuclCreateDate = entity.CuclCreateDate;
            customerClaim.CuclEvents = entity.CuclEvents;
            customerClaim.CuclEventPrice = entity.CuclEventPrice;
            customerClaim.CuclSubtotal = entity.CuclSubtotal;
            customerClaim.CuclReason = entity.CuclReason;

            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
        }
    }
}

using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Domain.Enum;
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

        public async Task<CustomerRequestResponseDto> ClaimPolis(CustomerClaimRequestDto customerClaimDto)
        {
            var existRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetById(customerClaimDto.CreqEntityid, true);
            if (existRequest == null)
                throw new EntityNotFoundException(customerClaimDto.CreqEntityid, "CustomerClaim");

            existRequest.CreqType = EnumCustomerRequest.CREQTYPE.CLAIM.ToString();
            existRequest.CreqModifiedDate = DateTime.Now;

            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
            var customerResponseDto = existRequest.Adapt<CustomerRequestResponseDto>();
            return customerResponseDto;
        }

        public async Task<CustomerRequestResponseDto> ClosePolis(CustomerCloseRequestDto customerCloseDto)
        {
            var existRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetById(customerCloseDto.CreqEntityid, true);
            if (existRequest == null)
                throw new EntityNotFoundException(customerCloseDto.CreqEntityid, "CustomerClaim");

            existRequest.CreqType = EnumCustomerRequest.CREQTYPE.CLOSE.ToString();
            existRequest.CreqModifiedDate = DateTime.Now;

            var customerClaim = await _repositoryCustomerManager.CustomerClaimRepository.GetEntityById(customerCloseDto.CreqEntityid, true);
            customerClaim.CuclReason = customerCloseDto.CuclReason;
            customerClaim.CuclCreateDate = DateTime.Now;

            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
            var customerResponseDto = existRequest.Adapt<CustomerRequestResponseDto>();
            return customerResponseDto;
        }

        public async Task<CustomerClaimDto> CreateAsync(CustomerClaimDto entity)
        {
            var customerClaim = entity.Adapt<CustomerClaim>();
            _repositoryCustomerManager.CustomerClaimRepository.CreateEntity(customerClaim);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

            return customerClaim.Adapt<CustomerClaimDto>();
        }

        public CustomerClaim CreateNewClaim(CustomerRequest customerRequest)
        {
            var newCustomerClaim = new CustomerClaim()
            {
                CuclCreqEntityid = customerRequest.CreqEntityid,
                CuclEvents = 0,
                CuclEventPrice = 0,
                CuclSubtotal = 0,
                CuclCreqEntity = customerRequest
            };

            return newCustomerClaim;
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

        public async Task<CustomerClaimResponseDto> GetClaimById(int cuclCreqEntityId)
        {
            var existClaim = await _repositoryCustomerManager.CustomerClaimRepository.GetEntityById(cuclCreqEntityId, false);
            if (existClaim == null)
                throw new EntityNotFoundException(cuclCreqEntityId, "CustomerClaim");

            var customerClaimResponseDto = existClaim.Adapt<CustomerClaimResponseDto>();
            return customerClaimResponseDto;
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

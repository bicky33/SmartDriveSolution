using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Domain.Entities.Users;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories.CR;
using Domain.Repositories.Master;
using Domain.Repositories.UserModule;
using Mapster;
using Microsoft.AspNetCore.Components.Forms;
using Service.Abstraction.CR;
using Service.Abstraction.User;
using Service.UserModule;
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
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public CustomerRequestService(
            IRepositoryCustomerManager repositoryCustomerManager,
            IRepositoryManagerUser repositoryManagerUser,
            IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryCustomerManager = repositoryCustomerManager;
            _repositoryManagerUser = repositoryManagerUser;
            _repositoryManagerMaster = repositoryManagerMaster;
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

        //public async Task<CustomerRequestDto> Create(CustomerRequestDto entity)
        //{
        //    var businessEntity = _repositoryManagerUser.BusinessEntityRepository.CreateEntity();
        //    await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

        //    // Create new CustomerInscAsset
        //    var customerInscAsset = entity.CustomerInscAsset.Adapt<CustomerInscAsset>();
        //    var newCustomerInscAsset = _repositoryCustomerManager.CustomerInscAssetRepository.CreateData(customerInscAsset);

        //    // Create new CustomerClaim
        //    var customerClaim = new CustomerClaimDto();
        //    var newCustomerClaim = _repositoryCustomerManager.CustomerClaimRepository.CreateData(customerClaim.Adapt<CustomerClaim>());

        //    //var customerRequest = entity.Adapt<CustomerRequest>();
        //    var customerRequest = new CustomerRequest()
        //    {
        //        CreqEntityid = businessEntity.Entityid,
        //        CreqCreateDate = DateTime.Now,
        //        CreqStatus = entity.CreqStatus,
        //        CreqType = entity.CreqType,
        //        CreqCustEntityid = entity.CreqCustEntityid,
        //        CreqAgenEntityid = entity.CreqAgenEntityid,
        //        CustomerInscAsset = newCustomerInscAsset,
        //        CustomerClaim = newCustomerClaim
        //    };

        //    _repositoryCustomerManager.CustomerRequestRepository.CreateEntity(customerRequest);
        //    await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

        //    return customerRequest.Adapt<CustomerRequestDto>();
        //}

        public async Task<CustomerRequestResponseDto> CreateByUser(CustomerRequestRequestDto entity)
        {
            var customerInscAssetDto = entity.CustomerInscAsset.Adapt<CustomerInscAsset>();

            // validate police number
            //await _serviceCustomerManager.CustomerInscAssetService.ValidatePoliceNumber(customerInscAssetDto.CiasPoliceNumber);
            var existPoliceNumber = await _repositoryCustomerManager.CustomerInscAssetRepository.FindByCiasPoliceNumber(customerInscAssetDto.CiasPoliceNumber, false);
            if (existPoliceNumber != null)
                throw new Exception($"Customer Request with police number {customerInscAssetDto.CiasPoliceNumber} is already exist");

            // check exist user
            var existUser = await _repositoryManagerUser.UserRepository.GetEntityById(entity.CreqCustEntityid, false);

            // check exist car
            var carSeries = await _repositoryManagerMaster.CarSeriesRepository.GetEntityById((int)customerInscAssetDto.CiasCarsId, false);
            if (carSeries == null)
                throw new EntityNotFoundException(customerInscAssetDto.CiasCarsId, "CarSeries");

            //// check exist city
            var cities = await _repositoryManagerMaster.CityRepository.GetEntityById((int)customerInscAssetDto.CiasCityId, false);
            if (cities == null)
                throw new EntityNotFoundException(customerInscAssetDto.CiasCityId, "Cities");

            //// check exist insurance type
            var insuranceType = await _repositoryManagerMaster.InsuranceTypeRepository.GetEntityByNameMaster(customerInscAssetDto.CiasIntyName, false);
            if (insuranceType == null)
                throw new EntityNotFoundException(customerInscAssetDto.CiasIntyName, "InsuranceType");

            // new businessEntity
            var businessEntity = _repositoryManagerUser.BusinessEntityRepository.CreateEntity();
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

            // create new customerInscAsset            
            //var customerInscAsset = customerInscAssetDto.Adapt<CustomerInscAsset>();
            var newCustomerInscAsset = _repositoryCustomerManager.CustomerInscAssetRepository.CreateData(customerInscAssetDto);
            newCustomerInscAsset.CiasStartdate = DateTime.Now;
            newCustomerInscAsset.CiasEnddate = DateTime.Now.AddYears(1);

            // create new customerClaim
            var customerClaim = new CustomerClaimDto();
            var newCustomerClaim = _repositoryCustomerManager.CustomerClaimRepository.CreateData(customerClaim.Adapt<CustomerClaim>());

            var newCustomerRequest = new CustomerRequest()
            {
                CreqEntityid = businessEntity.Entityid,
                CreqCreateDate = DateTime.Now,
                CreqStatus = EnumCustomerRequest.CREQSTATUS.OPEN.ToString(),
                CreqType = EnumCustomerRequest.CREQTYPE.POLIS.ToString(),
                CreqCustEntityid = existUser.UserEntityid,
                CreqAgenEntityid = entity.CreqAgenEntityid,
                CustomerInscAsset = newCustomerInscAsset,
                CustomerClaim = newCustomerClaim
            };

            _repositoryCustomerManager.CustomerRequestRepository.CreateEntity(newCustomerRequest);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
            return newCustomerRequest.Adapt<CustomerRequestResponseDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var customerRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetEntityById(id, false);
            if (customerRequest == null)
                throw new EntityNotFoundException(id, "CustomerRequest");
            
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

        //public async Task<CustomerRequest> CreateCustomerRequest(BusinessEntity newEntity, User customer, int entityId)
        //{
        //    CustomerRequest customerRequest = new CustomerRequest
        //    {
        //        CreqEntity = newEntity,
        //        CreqCustEntity = customer,
        //        CreqCreateDate = DateTime.Now,
        //        CreqStatus = EnumCustomerRequest.CREQSTATUS.OPEN.ToString(),
        //        CreqType = EnumCustomerRequest.CREQTYPE.POLIS.ToString(),
        //        CreqEntityid = entityId
        //    };

        //    return customerRequest;
        //}
    }
}

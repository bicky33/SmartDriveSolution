using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
using Contract.DTO.SO;
using Domain.Entities.CR;
using Domain.Entities.Users;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories.CR;
using Domain.Repositories.Master;
using Domain.Repositories.UserModule;
using Domain.RequestFeatured;
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
using System.Transactions;

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

        public async Task<CustomerRequestDto> CreateRequestByAgen(CreateRequestByAgenDto entity)
        {
            if (entity.IsGranted)
            {
                using var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
                try
                {
                    var newUserBusinessEntity = _repositoryManagerUser.BusinessEntityRepository.CreateEntity();
                    await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();
                    var user = new User()
                    {
                        UserEntityid = newUserBusinessEntity.Entityid,
                        UserName = entity.CustomerDto.PhoneNumber,
                        UserFullName = entity.CustomerDto.CustomerName,
                        UserPassword = BCrypt.Net.BCrypt.HashPassword(entity.CustomerDto.PhoneNumber),
                        UserEmail = entity.CustomerDto.PhoneNumber,
                        UserNationalId = entity.CustomerDto.PhoneNumber,
                        UserNpwp = entity.CustomerDto.PhoneNumber
                    };
                    _repositoryManagerUser.UserRepository.CreateEntity(user);
                    await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

                    var newUserRole = new UserRole()
                    {
                        UsroEntityid = newUserBusinessEntity.Entityid,
                        UsroRoleName = EnumRoleType.CU.ToString(),
                        UsroStatus = EnumRoleActiveStatus.ACTIVE.ToString()
                    };
                    _repositoryManagerUser.UserRoleRepository.CreateEntity(newUserRole);
                    await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

                    var newUserPhone = new UserPhone()
                    {
                        UsphEntityid = newUserBusinessEntity.Entityid,
                        UsphPhoneNumber = entity.CustomerDto.PhoneNumber
                    };
                    _repositoryManagerUser.UserPhoneRepository.CreateEntity(newUserPhone);
                    await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

                    var newRequestBusinessEntity = _repositoryManagerUser.BusinessEntityRepository.CreateEntity();
                    await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

                    var customerRequest = new CustomerRequest()
                    {
                        CreqEntityid = newRequestBusinessEntity.Entityid,
                        CreqCreateDate = entity.CreqCreateDate,
                        CreqStatus = EnumCustomerRequest.CREQSTATUS.OPEN.ToString(),
                        CreqType = EnumCustomerRequest.CREQTYPE.FEASIBILITY.ToString(),
                        CreqCustEntityid = newUserBusinessEntity.Entityid,
                        CreqAgenEntityid = 81
                    };
                    _repositoryCustomerManager.CustomerRequestRepository.CreateEntity(customerRequest);
                    await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

                    var customerInscAsset = new CustomerInscAsset()
                    {
                        CiasCreqEntityid = newRequestBusinessEntity.Entityid,
                        CiasPoliceNumber = entity.CustomerInscAsset.CiasPoliceNumber,
                        CiasYear = entity.CustomerInscAsset.CiasYear,
                        CiasStartdate = entity.CustomerInscAsset.CiasStartdate,
                        CiasEnddate = entity.CustomerInscAsset.CiasStartdate.AddYears(1),
                        CiasCurrentPrice = entity.CustomerInscAsset.CiasCurrentPrice,
                        CiasInsurancePrice = entity.CustomerInscAsset.CiasCurrentPrice,
                        CiasTotalPremi = entity.CustomerInscAsset.CiasTotalPremi,
                        CiasPaidType = entity.CustomerInscAsset.CiasPaidType,
                        CiasIsNewChar = entity.CustomerInscAsset.CiasIsNewChar,
                        CiasCarsId = entity.CustomerInscAsset.CiasCarsId,
                        CiasIntyName = entity.CustomerInscAsset.CiasIntyName,
                        CiasCityId = entity.CustomerInscAsset.CiasCityId
                    };
                    _repositoryCustomerManager.CustomerInscAssetRepository.CreateEntity(customerInscAsset);
                    await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
                    transaction.Complete();

                    var response = customerRequest.Adapt<CustomerRequestDto>();
                    return response;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                using var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
                try
                {
                    var newUserBusinessEntity = _repositoryManagerUser.BusinessEntityRepository.CreateEntity();
                    await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();
                    var user = new User()
                    {
                        UserEntityid = newUserBusinessEntity.Entityid,
                        UserName = entity.CustomerDto.PhoneNumber,
                        UserFullName = entity.CustomerDto.CustomerName,
                        UserEmail = entity.CustomerDto.PhoneNumber,
                        UserNationalId = entity.CustomerDto.PhoneNumber,
                        UserNpwp = entity.CustomerDto.PhoneNumber
                    };
                    _repositoryManagerUser.UserRepository.CreateEntity(user);
                    await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

                    var newUserRole = new UserRole()
                    {
                        UsroEntityid = newUserBusinessEntity.Entityid,
                        UsroRoleName = EnumRoleType.PC.ToString(),
                        UsroStatus = EnumRoleActiveStatus.ACTIVE.ToString()
                    };
                    _repositoryManagerUser.UserRoleRepository.CreateEntity(newUserRole);
                    await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

                    var newUserPhone = new UserPhone()
                    {
                        UsphEntityid = newUserBusinessEntity.Entityid,
                        UsphPhoneNumber = entity.CustomerDto.PhoneNumber
                    };
                    _repositoryManagerUser.UserPhoneRepository.CreateEntity(newUserPhone);
                    await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

                    var newRequestBusinessEntity = _repositoryManagerUser.BusinessEntityRepository.CreateEntity();
                    await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

                    var customerRequest = new CustomerRequest()
                    {
                        CreqEntityid = newRequestBusinessEntity.Entityid,
                        CreqCreateDate = entity.CreqCreateDate,
                        CreqStatus = EnumCustomerRequest.CREQSTATUS.OPEN.ToString(),
                        CreqType = EnumCustomerRequest.CREQTYPE.FEASIBILITY.ToString(),
                        CreqCustEntityid = newUserBusinessEntity.Entityid,
                        CreqAgenEntityid = 81
                    };
                    _repositoryCustomerManager.CustomerRequestRepository.CreateEntity(customerRequest);
                    await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

                    var customerInscAsset = new CustomerInscAsset()
                    {
                        CiasCreqEntityid = newRequestBusinessEntity.Entityid,
                        CiasPoliceNumber = entity.CustomerInscAsset.CiasPoliceNumber,
                        CiasYear = entity.CustomerInscAsset.CiasYear,
                        CiasStartdate = entity.CustomerInscAsset.CiasStartdate,
                        CiasEnddate = entity.CustomerInscAsset.CiasStartdate.AddYears(1),
                        CiasCurrentPrice = entity.CustomerInscAsset.CiasCurrentPrice,
                        CiasInsurancePrice = entity.CustomerInscAsset.CiasCurrentPrice,
                        CiasTotalPremi = entity.CustomerInscAsset.CiasTotalPremi,
                        CiasPaidType = entity.CustomerInscAsset.CiasPaidType,
                        CiasIsNewChar = entity.CustomerInscAsset.CiasIsNewChar,
                        CiasCarsId = entity.CustomerInscAsset.CiasCarsId,
                        CiasIntyName = entity.CustomerInscAsset.CiasIntyName,
                        CiasCityId = entity.CustomerInscAsset.CiasCityId
                    };
                    _repositoryCustomerManager.CustomerInscAssetRepository.CreateEntity(customerInscAsset);
                    await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
                    transaction.Complete();

                    var response = customerRequest.Adapt<CustomerRequestDto>();
                    return response;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<CustomerRequestDto> CreateRequestByCustomer(CreateRequestByCustomerDto entity)
        {
            using var transaction = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                var newBusinessEntity = _repositoryManagerUser.BusinessEntityRepository.CreateEntity();
                await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

                var customerRequest = new CustomerRequest()
                {
                    CreqEntityid = newBusinessEntity.Entityid,
                    CreqCreateDate = entity.CreqCreateDate,
                    CreqStatus = EnumCustomerRequest.CREQSTATUS.OPEN.ToString(),
                    CreqType = EnumCustomerRequest.CREQTYPE.FEASIBILITY.ToString(),
                    CreqCustEntityid = 1134,
                    CreqAgenEntityid = 81
                };
                _repositoryCustomerManager.CustomerRequestRepository.CreateEntity(customerRequest);
                await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

                var customerInscAsset = new CustomerInscAsset()
                {
                    CiasCreqEntityid = newBusinessEntity.Entityid,
                    CiasPoliceNumber = entity.CustomerInscAsset.CiasPoliceNumber,
                    CiasYear = entity.CustomerInscAsset.CiasYear,
                    CiasStartdate = entity.CustomerInscAsset.CiasStartdate,
                    CiasEnddate = entity.CustomerInscAsset.CiasStartdate.AddYears(1),
                    CiasCurrentPrice = entity.CustomerInscAsset.CiasCurrentPrice,
                    CiasInsurancePrice = entity.CustomerInscAsset.CiasCurrentPrice,
                    CiasTotalPremi = entity.CustomerInscAsset.CiasTotalPremi,
                    CiasPaidType = entity.CustomerInscAsset.CiasPaidType,
                    CiasIsNewChar = entity.CustomerInscAsset.CiasIsNewChar,
                    CiasCarsId = entity.CustomerInscAsset.CiasCarsId,
                    CiasIntyName = entity.CustomerInscAsset.CiasIntyName,
                    CiasCityId = entity.CustomerInscAsset.CiasCityId
                };
                _repositoryCustomerManager.CustomerInscAssetRepository.CreateEntity(customerInscAsset);
                await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
                transaction.Complete();

                var response = customerRequest.Adapt<CustomerRequestDto>();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CustomerRequestDto> CreatePolis(CustomerPolisRequestDto entity)
        {
            var existRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetById(entity.CreqEntityid, true);
            if(existRequest == null)
            {
                throw new EntityNotFoundException(entity.CreqEntityid, "CustomerRequest");
            }

            if (existRequest.CreqType != "FEASIBILITY")
                throw new Exception("This Request is already Created.");

            existRequest.CreqType = EnumCustomerRequest.CREQTYPE.POLIS.ToString();
            existRequest.CreqModifiedDate = DateTime.Now;

            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
            var customerRequestDto = existRequest.Adapt<CustomerRequestDto>();
            customerRequestDto.Servs = existRequest.Services.Adapt<List<ServiceDto>>();
            return customerRequestDto;
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

        public async Task<IEnumerable<CustomerRequestDto>> GetAllByCustomer(int userId, bool trackChanges)
        {
            var customerRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetAllByCustomer(userId, false);
            var customerRequestDto = customerRequest.Adapt<IEnumerable<CustomerRequestDto>>();

            return customerRequestDto;
        }

        public async Task<CustomerRequestDto> GetByIdAsync(int id, bool trackChanges)
        {
            var customerRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetById(id, false);
            if(customerRequest == null)
            {
                throw new EntityNotFoundException(id, "CustomerRequest");
            }
            var customerRequestDto = customerRequest.Adapt<CustomerRequestDto>();
            customerRequestDto.Servs = customerRequest.Services.Adapt<List<ServiceDto>>();
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

        public async Task<IEnumerable<CustomerRequestDto>> GetAllPagingAsync(EntityParameter entityParameters, bool trackChanges)
        {
            var customerRequest = await _repositoryCustomerManager.CustomerRequestRepository.GetAllPaging(entityParameters, trackChanges);
            var customerRequestDto = customerRequest.Adapt<IEnumerable<CustomerRequestDto>>();
            return customerRequestDto;
        }
    }
}

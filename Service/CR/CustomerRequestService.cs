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

        public async Task<CustomerRequestDto> CreateRequest(CreateCustomerRequestDto entity)
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
                        CreqType = EnumCustomerRequest.CREQTYPE.POLIS.ToString(),
                        CreqCustEntityid = newUserBusinessEntity.Entityid
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
                        CreqType = EnumCustomerRequest.CREQTYPE.POLIS.ToString(),
                        CreqCustEntityid = newUserBusinessEntity.Entityid
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

                    var response = customerRequest.Adapt<CustomerRequestDto>();
                    return response;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

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

        public async Task<CustomerRequestResponseDto> CreateByEmployee(CreateCustomerRequestByAgenDto entity)
        {
            string role = entity.IsGranted ? EnumRoleType.PC.ToString() : EnumRoleType.CU.ToString();
            string statusRoles = entity.IsGranted ? EnumRoleActiveStatus.ACTIVE.ToString() : EnumRoleActiveStatus.INACTIVE.ToString();

            var businessEntity = _repositoryManagerUser.BusinessEntityRepository.CreateEntity();
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

            var carSeries = await _repositoryManagerMaster.CarSeriesRepository.GetEntityById(entity.CarSeriesId, false);

            var cities = await _repositoryManagerMaster.CityRepository.GetEntityById(entity.CityId, false);

            var insuranceType = await _repositoryManagerMaster.InsuranceTypeRepository.GetEntityByNameMaster(entity.InsuranceType, false);

            // 
            var user = new User()
            {
                UserEntityid = businessEntity.Entityid,
                UserName = entity.PhoneNumber,
                UserPassword = BCrypt.Net.BCrypt.HashPassword(entity.PhoneNumber),
                UserFullName = entity.CustomerName,
                UserNpwp = entity.CustomerName,
                UserEmail = entity.CustomerName,
                UserNationalId = entity.PhoneNumber,
                UserModifiedDate = DateTime.Now,
                UserBirthDate = DateTime.Now,
            };
            _repositoryManagerUser.UserRepository.CreateEntity(user);
            await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

            var userRoles = new UserRole()
            {
                UsroEntityid = businessEntity.Entityid,
                UsroRoleName = role,
                UsroStatus = statusRoles
            };
            _repositoryManagerUser.UserRoleRepository.CreateEntity(userRoles);
            await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

            var userPhone = new UserPhone()
            {
                UsphEntityid = businessEntity.Entityid,
                UsphPhoneNumber = entity.PhoneNumber
            };
            _repositoryManagerUser.UserPhoneRepository.CreateEntity(userPhone);
            await _repositoryManagerUser.UnitOfWork.SaveChangesAsync();

            var customerInscAsset = new CustomerInscAsset()
            {
                CiasCreqEntityid = businessEntity.Entityid,
                CiasPoliceNumber = entity.PoliceNumber,
                CiasYear = entity.CarYear,
                CiasStartdate = DateTime.Now,
                CiasEnddate = DateTime.Now.AddYears(1),
                CiasCurrentPrice = entity.CurrentPrice,
                CiasPaidType = entity.PaidType.ToString(),
                CiasIsNewChar = entity.IsNewChar,
                CiasCarsId = carSeries.CarsId,
                CiasCityId = cities.CityId,
                CiasIntyName = insuranceType.IntyName
            };
            _repositoryCustomerManager.CustomerInscAssetRepository.CreateEntity(customerInscAsset);

            // customr claim tidak di create
            var customerClaim = new CustomerClaim()
            {
                CuclCreqEntityid = businessEntity.Entityid
            };
            _repositoryCustomerManager.CustomerClaimRepository.CreateEntity(customerClaim);

            var newCustomerRequest = new CustomerRequest()
            {
                CreqEntityid = businessEntity.Entityid,
                CreqCreateDate = DateTime.Now,
                CreqStatus = EnumCustomerRequest.CREQSTATUS.OPEN.ToString(),
                CreqType = EnumCustomerRequest.CREQTYPE.POLIS.ToString(),
                CreqCustEntityid = user.UserEntityid,
                CreqAgenEntityid = entity.CreqAgenEntityid,
                CustomerInscAsset = customerInscAsset,
                CustomerClaim = customerClaim
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
    }
}

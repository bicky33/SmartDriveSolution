using Contract.DTO.CR.Request;
using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.CR;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;
using Service.Abstraction.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Service.CR
{
    public class CustomerInscAssetsService : ICustomerInscAssetService
    {
        private readonly IRepositoryCustomerManager _repositoryCustomerManager;
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public CustomerInscAssetsService(IRepositoryCustomerManager repositoryCustomerManager, IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryCustomerManager = repositoryCustomerManager;
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<CustomerInscAssetDto> CreateAsync(CustomerInscAssetDto entity)
        {
            var customerInscAsset = entity.Adapt<CustomerInscAsset>();
            _repositoryCustomerManager.CustomerInscAssetRepository.CreateEntity(customerInscAsset);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

            return customerInscAsset.Adapt<CustomerInscAssetDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var customerInscAsset = await _repositoryCustomerManager.CustomerInscAssetRepository.GetEntityById(id, false);
            if (customerInscAsset == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscAsset");
            }

            _repositoryCustomerManager.CustomerInscAssetRepository.DeleteEntity(customerInscAsset);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerInscAssetDto>> GetAllAsync(bool trackChanges)
        {
            var customerInscAsset = await _repositoryCustomerManager.CustomerInscAssetRepository.GetAllEntity(false);
            var customerInscAssetDto = customerInscAsset.Adapt<IEnumerable<CustomerInscAssetDto>>();

            return customerInscAssetDto;
        }

        public async Task<CustomerInscAssetDto> GetByIdAsync(int id, bool trackChanges)
        {
            var customerInscAsset = await _repositoryCustomerManager.CustomerInscAssetRepository.GetEntityById(id, false);
            if(customerInscAsset == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscAsset");
            }
            var customerInscAssetDto = customerInscAsset.Adapt<CustomerInscAssetDto>();
            return customerInscAssetDto;
        }

        public async Task UpdateAsync(int id, CustomerInscAssetDto entity)
        {
            var customerInscAsset = await _repositoryCustomerManager.CustomerInscAssetRepository.GetEntityById(id, true);
            if (customerInscAsset == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscAsset");
            }

            customerInscAsset.CiasPoliceNumber = entity.CiasPoliceNumber;
            customerInscAsset.CiasYear = entity.CiasYear;
            customerInscAsset.CiasStartdate = entity.CiasStartdate;
            customerInscAsset.CiasEnddate = entity.CiasEnddate;
            customerInscAsset.CiasCurrentPrice = entity.CiasCurrentPrice;
            customerInscAsset.CiasInsurancePrice = entity.CiasInsurancePrice;
            customerInscAsset.CiasTotalPremi = entity.CiasTotalPremi;
            customerInscAsset.CiasPaidType = entity.CiasPaidType;
            customerInscAsset.CiasIsNewChar = entity.CiasIsNewChar;
            customerInscAsset.CiasCarsId = entity.CiasCarsId;
            customerInscAsset.CiasIntyName = entity.CiasIntyName;
            customerInscAsset.CiasCityId = entity.CiasCityId;

            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
        }

        public CustomerInscAsset CreateCustomerInscAssets(
            int entityId,
            CustomerInscAssetRequestDto customerInscAssetRequestDto,
            CarSeries carSeries,
            City existCity,
            InsuranceType existInty,
            CustomerRequest newCustomerRequest)
        {

            DateTime ciasStartdate = DateTime.Now;

            // new cias
            var customrInscAsset = new CustomerInscAsset()
            {
                CiasCreqEntityid = entityId,
                CiasPoliceNumber = customerInscAssetRequestDto.CiasPoliceNumber,
                CiasYear = customerInscAssetRequestDto.CiasYear,
                CiasStartdate = ciasStartdate,
                CiasEnddate = ciasStartdate.AddYears(1),
                CiasCurrentPrice = customerInscAssetRequestDto.CiasCurrentPrice,
                CiasInsurancePrice = customerInscAssetRequestDto.CiasCurrentPrice,
                CiasPaidType = customerInscAssetRequestDto.CiasPaidType,
                CiasIsNewChar = customerInscAssetRequestDto.CiasIsNewChar,
                CiasCars = carSeries,
                CiasCity = existCity,
                CiasIntyNameNavigation = existInty,
                CiasCreqEntity = newCustomerRequest
            };

            return customrInscAsset;
        }

        public decimal? GetPremiPrice(string insuraceType, string carBrand, int zonesId, decimal currentPrice, int ageOfBirth, List<CustomerInscExtend> cuexs)
        {
            List<string> carBrandRateMax = new List<string> { "BMW", "MERCEDEZ BENZ", "AUDI", "VOLKSWAGEN", "LAND ROVER", "JAGUAR", "PEUGOT", "RENAULT", "SMART", "VOLVO", "MINI", "FLAT", "OPEN", "MAZDA" };

            var temiMain = _repositoryManagerMaster.TemplateInsurancePremiRepository.GetTemiByCateIDIntyNameZoneID(1, insuraceType, zonesId, false) ?? throw new Exception("Template Insurance Premi is not found");
            var templateInsuracePremi = temiMain.Adapt<TemplateInsurancePremi>();

            int yearsNow = DateTime.Now.Year;
            int userAge = yearsNow - ageOfBirth;

            double? temiRate;

            if (userAge >= 17 && userAge <= 27)
            {
                temiRate = templateInsuracePremi.TemiRateMax;
            }
            else
            {
                if (carBrandRateMax.Contains(carBrand))
                {
                    temiRate = templateInsuracePremi.TemiRateMax;
                }
                else
                {
                    temiRate = templateInsuracePremi.TemiRateMin;
                }
            }

            double? rate = temiRate / 100;
            decimal rateBig = (decimal)rate;
            decimal premiMain = currentPrice * rateBig;

            decimal? premiExtend = 0;
            decimal materai = 10000;

            if (cuexs != null && cuexs.Any())
            {
                foreach (CustomerInscExtend cuex in cuexs)
                {
                    premiExtend = premiExtend + (cuex.CuexNominal);
                }
            }

            decimal? totalPremi = premiMain + premiExtend;
            decimal? result = totalPremi + materai;

            return result;
        }

        public async Task ValidatePoliceNumber(string policeNumber)
        {
            var existingCustomerInscAssets = await _repositoryCustomerManager.CustomerInscAssetRepository.FindByCiasPoliceNumber(policeNumber, false);
            if (existingCustomerInscAssets != null)
            {
                throw new Exception($"Customer Request with police number {policeNumber} is already exist");
            }
        }


    }
}

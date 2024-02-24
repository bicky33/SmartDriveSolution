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

        public async Task<decimal> GetPremiPrice(string insuranceType, int carSeriesId, int cityId, decimal currentPrice)
        {
            List<string> carBrandRateMax = new List<string> { "BMW", "MERCEDEZ BENZ", "AUDI", "VOLKSWAGEN", "LAND ROVER", "JAGUAR", "PEUGOT", "RENAULT", "SMART", "VOLVO", "MINI", "FLAT", "OPEN", "MAZDA" };

            var carSeries = await _repositoryManagerMaster.CarSeriesRepository.GetEntityById(carSeriesId, false);
            var carModel = await _repositoryManagerMaster.CarModelRepository.GetEntityById((int)carSeries.CarsCarmId, false);
            var carBrand = await _repositoryManagerMaster.CarBrandRepository.GetEntityById((int)carModel.CarmCabrId, false);

            var cities = await _repositoryManagerMaster.CityRepository.GetEntityById(cityId, false);
            var province = await _repositoryManagerMaster.ProvinsiRepository.GetEntityById((int)cities.CityProvId, false);
            var zones = await _repositoryManagerMaster.ZoneRepository.GetEntityById((int)province.ProvZonesId, false);

            var templateInsurancePremi = await _repositoryManagerMaster.TemplateInsurancePremiRepository.GetTemiByCateIDIntyNameZoneID(1, insuranceType, zones.ZonesId, false) ?? throw new Exception("Template Insurance Premi is not found");

            double temiRate;

            if (carBrandRateMax.Contains(carBrand.CabrName))
            {
                temiRate = (double)templateInsurancePremi.TemiRateMax;
            }
            else
            {
                temiRate = (double)templateInsurancePremi.TemiRateMin;
            }
        
            double rate = temiRate / 100;
            decimal rateBig = (decimal)rate;
            decimal premiMain = currentPrice * rateBig;

            decimal materai = 10000;

            decimal totalPremi = premiMain + materai;

            return totalPremi;
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

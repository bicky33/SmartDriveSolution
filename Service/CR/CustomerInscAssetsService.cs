using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Domain.Exceptions;
using Domain.Repositories.CR;
using Mapster;
using Service.Abstraction.Base;
using Service.Abstraction.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CR
{
    public class CustomerInscAssetsService : ICustomerInscAssetService
    {
        private readonly IRepositoryCustomerManager _repositoryCustomerManager;

        public CustomerInscAssetsService(IRepositoryCustomerManager repositoryCustomerManager)
        {
            _repositoryCustomerManager = repositoryCustomerManager;
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
    }
}

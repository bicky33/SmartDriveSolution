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
    public class CustomerInscExtendService : ICustomerInscExtendService
    {
        private readonly IRepositoryCustomerManager _repositoryCustomerManager;

        public CustomerInscExtendService(IRepositoryCustomerManager repositoryCustomerManager)
        {
            _repositoryCustomerManager = repositoryCustomerManager;
        }

        public async Task<CustomerInscExtendDto> CreateAsync(CustomerInscExtendDto entity)
        {
            var customerInscExtend = entity.Adapt<CustomerInscExtend>();
            _repositoryCustomerManager.CustomerInscExtendRepository.CreateEntity(customerInscExtend);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

            return customerInscExtend.Adapt<CustomerInscExtendDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var customerInscExtend = await _repositoryCustomerManager.CustomerInscExtendRepository.GetEntityById(id, false);
            if (customerInscExtend == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscExtend");
            }

            _repositoryCustomerManager.CustomerInscExtendRepository.DeleteEntity(customerInscExtend);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerInscExtendDto>> GetAllAsync(bool trackChanges)
        {
            var customerInscExtend = await _repositoryCustomerManager.CustomerInscExtendRepository.GetAllEntity(false);
            var customerInscExtendDto = customerInscExtend.Adapt<IEnumerable<CustomerInscExtendDto>>();

            return customerInscExtendDto;
        }

        public async Task<CustomerInscExtendDto> GetByIdAsync(int id, bool trackChanges)
        {
            var customerInscExtend = await _repositoryCustomerManager.CustomerInscExtendRepository.GetEntityById(id, false);
            if (customerInscExtend == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscExtend");
            }

            var customerInscExtendDto = customerInscExtend.Adapt<CustomerInscExtendDto>();
            return customerInscExtendDto;
        }

        public async Task UpdateAsync(int id, CustomerInscExtendDto entity)
        {
            var customerInscExtend = await _repositoryCustomerManager.CustomerInscExtendRepository.GetEntityById(id, true);
            if (customerInscExtend == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscExtend");
            }

            customerInscExtend.CuexName = entity.CuexName;
            customerInscExtend.CuexTotalItem = entity.CuexTotalItem;
            customerInscExtend.CuexNominal = entity.CuexNominal;

            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
        }
    }
}

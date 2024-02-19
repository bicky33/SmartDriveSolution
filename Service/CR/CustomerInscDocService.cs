using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Domain.Exceptions;
using Domain.Repositories.CR;
using Mapster;
using Microsoft.AspNetCore.Components.Forms;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CR
{
    public class CustomerInscDocService : IServiceEntityBase<CustomerInscDocDto>
    {
        private readonly IRepositoryCustomerManager _repositoryCustomerManager;

        public CustomerInscDocService(IRepositoryCustomerManager repositoryCustomerManager)
        {
            _repositoryCustomerManager = repositoryCustomerManager;
        }

        public async Task<CustomerInscDocDto> CreateAsync(CustomerInscDocDto entity)
        {
            var customerInscDoc = entity.Adapt<CustomerInscDoc>();
            _repositoryCustomerManager.CustomerInscDocRepository.CreateEntity(customerInscDoc);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();

            return customerInscDoc.Adapt<CustomerInscDocDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var customerInscDoc = await _repositoryCustomerManager.CustomerInscDocRepository.GetEntityById(id, false);
            if (customerInscDoc == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscDoc");
            }

            _repositoryCustomerManager.CustomerInscDocRepository.DeleteEntity(customerInscDoc);
            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerInscDocDto>> GetAllAsync(bool trackChanges)
        {
            var customerInscDoc = await _repositoryCustomerManager.CustomerInscDocRepository.GetAllEntity(false);
            var customerInscDocDto = customerInscDoc.Adapt<IEnumerable<CustomerInscDocDto>>();

            return customerInscDocDto;
        }

        public async Task<CustomerInscDocDto> GetByIdAsync(int id, bool trackChanges)
        {
            var customerInscDoc = await _repositoryCustomerManager.CustomerInscDocRepository.GetEntityById(id, false);
            if (customerInscDoc == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscDoc");
            }

            var customerInscDocDto = customerInscDoc.Adapt<CustomerInscDocDto>();
            return customerInscDocDto;
        }

        public async Task UpdateAsync(int id, CustomerInscDocDto entity)
        {
            var customerInscDoc = await _repositoryCustomerManager.CustomerInscDocRepository.GetEntityById(id, true);
            if (customerInscDoc == null)
            {
                throw new EntityNotFoundException(id, "CustomerInscDoc");
            }

            customerInscDoc.CadocFilename = entity.CadocFilename;
            customerInscDoc.CadocFiletype = entity.CadocFiletype;
            customerInscDoc.CadocFilesize = entity.CadocFilesize;
            customerInscDoc.CadocCategory = entity.CadocCategory;
            customerInscDoc.CadocModifiedDate = entity.CadocModifiedDate;

            await _repositoryCustomerManager.CustomerUnitOfWork.SaveChangesAsync();
        }
    }
}

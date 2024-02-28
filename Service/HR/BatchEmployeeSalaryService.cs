using Contract.DTO.HR;
using Domain.Entities.HR;
using Domain.Exceptions;
using Domain.Repositories.HR;
using Domain.RequestFeatured;
using Mapster;
using Service.Abstraction.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.HR
{
    public class BatchEmployeeSalaryService : IBatchEmployeeSalaryService
    {
        private readonly IRepositoryHRManager _repositoryManager;

        public BatchEmployeeSalaryService(IRepositoryHRManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<BatchEmployeeSalaryDto> CreateAsync(BatchEmployeeSalaryDto entity)
        {
            var data = entity.Adapt<BatchEmployeeSalary>();
            _repositoryManager.BatchEmployeeSalaryRepository.CreateEntity(data);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            return data.Adapt<BatchEmployeeSalaryDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var besa = await _repositoryManager.BatchEmployeeSalaryRepository.GetEntityById(id, false);
            if (besa == null)
            {
                throw new EntityNotFoundException(id, nameof(BatchEmployeeSalary));
            }
            _repositoryManager.BatchEmployeeSalaryRepository.DeleteEntity(besa);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<BatchEmployeeSalaryDto>> GetAllAsync(bool trackChanges)
        {
            var besa = await _repositoryManager.BatchEmployeeSalaryRepository.GetAllEntity(false);
            var besaDto = besa.Adapt<IEnumerable<BatchEmployeeSalaryDto>>();
            return besaDto;
        }

        public async Task<IEnumerable<BatchEmployeeSalaryDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges)
        {
            var besa = await _repositoryManager.BatchEmployeeSalaryRepository.GetAllPaging(entityParameter, trackChanges);

            var besaDto = besa.Adapt<IEnumerable<BatchEmployeeSalaryDto>>();

            return besaDto;
        }

        public async Task<BatchEmployeeSalaryDto> GetByIdAsync(int id, bool trackChanges)
        {
            var besa = await _repositoryManager.BatchEmployeeSalaryRepository.GetEntityById(id, false);
            if (besa == null)
            {
                throw new EntityNotFoundException(id, nameof(BatchEmployeeSalaryDto));
            }

            var besaDto = besa.Adapt<BatchEmployeeSalaryDto>();

            return besaDto;
        }

        public async Task UpdateAsync(int id, BatchEmployeeSalaryDto entity)
        {
            var arwg = await _repositoryManager.BatchEmployeeSalaryRepository.GetEntityById(id, false);
            if (arwg == null)
            {
                throw new EntityNotFoundException(id, nameof(BatchEmployeeSalary));
            }

            /*arwg.EawgEntityid = entity.EawgEntityid;
            arwg.EawgStatus = entity.EawgStatus;
            arwg.EawgArwgCode = entity.EawgArwgCode;
            arwg.EawgModifiedDate = entity.EawgModifiedDate;*/
            arwg = entity.Adapt<BatchEmployeeSalary>();

            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
        }
    }
}

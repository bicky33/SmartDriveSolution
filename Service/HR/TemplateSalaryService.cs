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
    public class TemplateSalaryService : ITemplateSalaryService
    {
        private readonly IRepositoryHRManager _repositoryManager;

        public TemplateSalaryService(IRepositoryHRManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        public async Task<TemplateSalaryDto> CreateAsync(TemplateSalaryDto entity)
        {
            var data = entity.Adapt<TemplateSalary>();
            _repositoryManager.TemplateSalaryRepository.CreateEntity(data);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            return data.Adapt<TemplateSalaryDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var tesa = await _repositoryManager.TemplateSalaryRepository.GetEntityById(id, false);
            if (tesa == null)
            {
                throw new EntityNotFoundException(id, nameof(TemplateSalary));
            }
            _repositoryManager.TemplateSalaryRepository.DeleteEntity(tesa);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<TemplateSalaryDto>> GetAllAsync(bool trackChanges)
        {
            var tesa = await _repositoryManager.TemplateSalaryRepository.GetAllEntity(false);
            var tesaDto = tesa.Adapt<IEnumerable<TemplateSalaryDto>>();
            return tesaDto;
        }

        public async Task<IEnumerable<TemplateSalaryDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges)
        {
            var tesa = await _repositoryManager.TemplateSalaryRepository.GetAllPaging(entityParameter, trackChanges);

            var tesaDto = tesa.Adapt<IEnumerable<TemplateSalaryDto>>();

            return tesaDto;
        }

        public async Task<TemplateSalaryDto> GetByIdAsync(int id, bool trackChanges)
        {
            var tesa = await _repositoryManager.TemplateSalaryRepository.GetEntityById(id, false);
            if (tesa == null)
            {
                throw new EntityNotFoundException(id, nameof(TemplateSalaryDto));
            }

            var tesaDto = tesa.Adapt<TemplateSalaryDto>();

            return tesaDto;
        }

        public async Task UpdateAsync(int id, TemplateSalaryDto entity)
        {
            var tesa = await _repositoryManager.TemplateSalaryRepository.GetEntityById(id, false);
            if (tesa == null)
            {
                throw new EntityNotFoundException(id, nameof(TemplateSalary));
            }
            tesa = entity.Adapt<TemplateSalary>();

            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
        }
    }
}

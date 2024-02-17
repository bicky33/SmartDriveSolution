using Contract.DTO.HR;
using Domain.Entities.HR;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.HR;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Service.Abstraction.Base;
using Service.Abstraction.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HR
{

    public class JobTypeService : IJobTypeService
    {

        private readonly IRepositoryManager _repositoryManager;

        public JobTypeService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<JobTypeDto> CreateAsync(JobTypeDto entity)
        {
            var jobType = entity.Adapt<JobType>();
            _repositoryManager.JobTypeRepository.CreateEntity(jobType);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            return jobType.Adapt<JobTypeDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repositoryManager.JobTypeRepository.GetEntityById(id, false);
            if (category == null)
            {
                throw new EntityNotFoundException(id);
            }
            _repositoryManager.JobTypeRepository.DeleteEntity(category);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

        }

        public async Task DeleteDataAsync(string id)
        {
            var category = await _repositoryManager.JobTypeRepository.GetJobTypeById(id, false);
            

            _repositoryManager.JobTypeRepository.DeleteEntity(category);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobTypeDto>> GetAllAsync(bool trackChanges)
        {
            var categories = await _repositoryManager.JobTypeRepository.GetAllEntity(false);
            var categoryDto = categories.Adapt<IEnumerable<JobTypeDto>>();
            return categoryDto;
        }

        public async Task<JobTypeDto> GetByIdAsync(int id, bool trackChanges)
        {
            var category = await _repositoryManager.JobTypeRepository.GetEntityById(id, false);
            if (category == null)
            {
                throw new EntityNotFoundException(id);
            }

            var data = category.Adapt<JobTypeDto>();

            return data;
        }

        public async Task<JobTypeDto> GetJobTypeById(string id, bool trackChanges)
        {
            var category = await _repositoryManager.JobTypeRepository.GetJobTypeById(id, false);
           /* if (category == null)
            {
                return BadRequest();
            }*/

            var data = category.Adapt<JobTypeDto>();

            return data;
        }

        public async Task UpdateAsync(int id, JobTypeDto entity)
        {
            var category = await _repositoryManager.JobTypeRepository.GetEntityById(id, true);
            if (category == null)
            {
                throw new EntityNotFoundException(id);
            }

            category.JobCode = entity.JobCode;
            category.JobModifiedDate = entity.JobModifiedDate;
            category.JobDesc = entity.JobDesc;
            category.JobRateMin = entity.JobRateMin;
            category.JobRateMax = entity.JobRateMax;
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
        }

        public async  Task UpdateDataAsync(string id, JobTypeDto entity)
        {
            var category = await _repositoryManager.JobTypeRepository.GetJobTypeById(id, true);


            category.JobCode = entity.JobCode;
            category.JobModifiedDate = entity.JobModifiedDate;
            category.JobDesc = entity.JobDesc;
            category.JobRateMin = entity.JobRateMin;
            category.JobRateMax = entity.JobRateMax;
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
        }
    }
}

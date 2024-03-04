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
    public class EmployeeSalaryDetailService : IEmployeeSalaryDetailService
    {
        private readonly IRepositoryHRManager _repositoryManager;

        public EmployeeSalaryDetailService(IRepositoryHRManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<EmployeeSalaryDetailDto> CreateAsync(EmployeeSalaryDetailDto entity)
        {
            var data = entity.Adapt<EmployeeSalaryDetail>();
            _repositoryManager.EmployeeSalaryDetailRepository.CreateEntity(data);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            return data.Adapt<EmployeeSalaryDetailDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var emsa = await _repositoryManager.EmployeeSalaryDetailRepository.GetEntityById(id, false);
            if (emsa == null)
            {
                throw new EntityNotFoundException(id, nameof(EmployeeSalaryDetail));
            }
            _repositoryManager.EmployeeSalaryDetailRepository.DeleteEntity(emsa);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeSalaryDetailDto>> GetAllAsync(bool trackChanges)
        {
            var emsa = await _repositoryManager.EmployeeSalaryDetailRepository.GetAllEntity(false);
            var emsaDto = emsa.Adapt<IEnumerable<EmployeeSalaryDetailDto>>();
            return emsaDto;
        }

        public async Task<IEnumerable<EmployeeSalaryDetailDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges)
        {
            var emsa = await _repositoryManager.EmployeeSalaryDetailRepository.GetAllPaging(entityParameter, trackChanges);

            var emsaDto = emsa.Adapt<IEnumerable<EmployeeSalaryDetailDto>>();

            return emsaDto;
        }

        public async Task<EmployeeSalaryDetailDto> GetByIdAsync(int id, bool trackChanges)
        {
            var emsa = await _repositoryManager.EmployeeSalaryDetailRepository.GetEntityById(id, false);
            if (emsa == null)
            {
                throw new EntityNotFoundException(id, nameof(EmployeeSalaryDetail));
            }

            var emsaDto = emsa.Adapt<EmployeeSalaryDetailDto>();

            return emsaDto;
        }

        public async Task UpdateAsync(int id, EmployeeSalaryDetailDto entity)
        {
            var emsa = await _repositoryManager.EmployeeSalaryDetailRepository.GetEntityById(id, false);
            if (emsa == null)
            {
                throw new EntityNotFoundException(id, nameof(EmployeeSalaryDetail));
            }

            /*arwg.EawgEntityid = entity.EawgEntityid;
            arwg.EawgStatus = entity.EawgStatus;
            arwg.EawgArwgCode = entity.EawgArwgCode;
            arwg.EawgModifiedDate = entity.EawgModifiedDate;*/
            emsa = entity.Adapt<EmployeeSalaryDetail>();

            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
        }
    }
}

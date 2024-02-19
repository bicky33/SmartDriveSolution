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
    public class EmployeeArwgService : IEmployeeArwgService
    {
        private readonly IRepositoryHRManager _repositoryManager;

        public EmployeeArwgService(IRepositoryHRManager repositoryHRManager)
        {
            _repositoryManager = repositoryHRManager;
        }

        public async Task<EmployeeAreaWorkGroupDto> CreateAsync(EmployeeAreaWorkGroupDto entity)
        {

            var data = entity.Adapt<EmployeeAreWorkgroup>();
            _repositoryManager.EmployeeArwgRepository.CreateEntity(data);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            return data.Adapt<EmployeeAreaWorkGroupDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var arwg = await _repositoryManager.EmployeeArwgRepository.GetEntityById(id, false);
            if (arwg == null)
            {
                throw new EntityNotFoundException(id, nameof(EmployeeAreWorkgroup));
            }
            _repositoryManager.EmployeeArwgRepository.DeleteEntity(arwg);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

        }

        public async Task<IEnumerable<EmployeeAreaWorkGroupDto>> GetAllAsync(bool trackChanges)
        {
            var arwg = await _repositoryManager.EmployeeArwgRepository.GetAllEntity(false);
            var arwgDto = arwg.Adapt<IEnumerable<EmployeeAreaWorkGroupDto>>();
            return arwgDto;
        }

        public async Task<IEnumerable<EmployeeAreaWorkGroupDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges)
        {
            var arwg = await _repositoryManager.EmployeeArwgRepository.GetAllPaging(entityParameter, trackChanges);

            var arwgDto = arwg.Adapt<IEnumerable<EmployeeAreaWorkGroupDto>>();

            return arwgDto;
        }

        public async Task<EmployeeAreaWorkGroupDto> GetByIdAsync(int id, bool trackChanges)
        {
            var arwg = await _repositoryManager.EmployeeArwgRepository.GetEntityById(id, false);
            if (arwg == null)
            {
                throw new EntityNotFoundException(id, nameof(EmployeeAreWorkgroup));
            }

            var data = arwg.Adapt<EmployeeAreaWorkGroupDto>();

            return data;
        }

        public async Task UpdateAsync(int id, EmployeeAreaWorkGroupDto entity)
        {
            var arwg = await _repositoryManager.EmployeeArwgRepository.GetEntityById(id, true);
            if (arwg == null)
            {
                throw new EntityNotFoundException(id, nameof(EmployeeAreWorkgroup));
            }

            arwg.EawgEntityid = entity.EawgEntityid;
            arwg.EawgStatus = entity.EawgStatus;
            arwg.EawgArwgCode = entity.EawgArwgCode;
            arwg.EawgModifiedDate = entity.EawgModifiedDate;

            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

        }
    }
}

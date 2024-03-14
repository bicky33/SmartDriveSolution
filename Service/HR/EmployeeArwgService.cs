using Contract.DTO.HR;
using Contract.DTO.HR.CreateEawg;
using Domain.Entities.HR;
using Domain.Exceptions;
using Domain.Repositories.HR;
using Domain.RequestFeatured;
using Mapster;
using Service.Abstraction.Base;
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

        public async Task<ArwgEmployee> CreateArwg(ArwgEmployee entity)
        {
            var data = entity.EmployeeAreWorkgroups.FirstOrDefault().Adapt<EmployeeAreWorkgroup>();
            data.SoftDelete = "ACTIVE";
            _repositoryManager.EmployeeArwgRepository.CreateEntity(data);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            return data.Adapt<ArwgEmployee>();
        }

        public async Task<EmployeeAreaWorkGroupDto> CreateAsync(EmployeeAreaWorkGroupDto entity)
        {

            var data = entity.Adapt<EmployeeAreWorkgroup>();
            data.SoftDelete = "ACTIVE";
            _repositoryManager.EmployeeArwgRepository.CreateEntity(data);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            return data.Adapt<EmployeeAreaWorkGroupDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var arwg = await _repositoryManager.EmployeeArwgRepository.GetEntityById(id, true);
            if (arwg == null)
            {
                throw new EntityNotFoundException(id, nameof(EmployeeAreWorkgroup));
            }
            arwg.SoftDelete = "INACTIVE";
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

        }

        public async Task<EawgShowDto> FindEawgById(int id)
        {
            var employee = await _repositoryManager.EmployeeArwgRepository.FindArwgById(id);
            var employeeDto = employee.Adapt<EawgShowDto>();
            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeAreaWorkGroupShowDto>> FindEmployeeById(int id)
        {
            var arwg = await _repositoryManager.EmployeeArwgRepository.FindEmployeeById(id);
            var arwgDto = arwg.Adapt<IEnumerable<EmployeeAreaWorkGroupShowDto>>();
            return arwgDto;
        }

        public Task<IEnumerable<EmployeeAreaWorkGroupDto>> GetAllAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeAreaWorkGroupShowDto>> GetAllData(bool trackChanges)
        {
            var arwg = await _repositoryManager.EmployeeArwgRepository.GetAllEntity(trackChanges);
            var arwgDto = arwg.Adapt<IEnumerable<EmployeeAreaWorkGroupShowDto>>();
            return arwgDto;
        }

        public async Task<IEnumerable<EmployeeAreaWorkGroupShowDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges)
        {
            var arwg = await _repositoryManager.EmployeeArwgRepository.GetAllPaging(entityParameter, trackChanges);

            var arwgDto = arwg.Adapt<IEnumerable<EmployeeAreaWorkGroupShowDto>>();

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

        public async Task UpdateArwg(int id, ArwgEmployeeUpdateDto entity)
        {
            var arwg = await _repositoryManager.EmployeeArwgRepository.GetEntityById(id, true);
            if (arwg == null)
            {
                throw new EntityNotFoundException(id, nameof(EmployeeAreWorkgroup));
            }

            var updateData = entity.EmployeeAreWorkgroups.FirstOrDefault().Adapt<EmployeeAreWorkgroup>();


            arwg.EawgArwgCode = updateData.EawgArwgCode;
            arwg.EawgModifiedDate = DateTime.Now;

            await _repositoryManager.UnitOfWorks.SaveChangesAsync();
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

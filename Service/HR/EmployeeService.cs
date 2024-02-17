using Contract.DTO.HR;
using Domain.Repositories.HR;
using Domain.Repositories.HR.RequestFeature;
using Mapster;
using Service.Abstraction.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HR
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IRepositoryManager _repositoryManager;

        public EmployeeService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Task<EmployeeDto> CreateAsync(EmployeeDto entity)
        {
            throw new NotImplementedException();
        }
         
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync(bool trackChanges)
        {
            var employee = await _repositoryManager.EmployeeRepository.GetAllEntity(false);
            var employeeDto = employee.Adapt<IEnumerable<EmployeeDto>>();
            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges)
        {
            var categories = await _repositoryManager.EmployeeRepository.GetAllPaging(entityParameter, trackChanges);

            var categoryDto = categories.Adapt<IEnumerable<EmployeeDto>>();

            return categoryDto;
        }

        public async Task<EmployeeDto> GetByIdAsync(int id, bool trackChanges)
        {
            var category = await _repositoryManager.EmployeeRepository.GetEntityById(id, false);
           /* if (category == null)
            {
                throw new EntityBadRequestException(id, "Category");
            }*/

            var data = category.Adapt<EmployeeDto>();

            return data;
        }

        public Task UpdateAsync(int id, EmployeeDto entity)
        {
            throw new NotImplementedException();
        }
    }
}

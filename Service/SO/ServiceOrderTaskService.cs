using Contract.DTO.SO;
using Domain.Entities.SO;
using Domain.Exceptions;
using Domain.Repositories.SO;
using Mapster;
using Service.Abstraction.SO;

namespace Service.SO
{
    public class ServiceOrderTaskService : IServiceSOEntityBase<ServiceOrderTaskDto, ServiceOrderTaskDtoCreate, int>
    {
        private readonly IRepositorySOManager _repositoryManager;

        public ServiceOrderTaskService(IRepositorySOManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ServiceOrderTaskDtoCreate> CreateAsync(ServiceOrderTaskDtoCreate entity)
        {
            var service = entity.Adapt<Domain.Entities.SO.ServiceOrderTask>();
            _repositoryManager.ServiceOrderTaskRepository.CreateEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return service.Adapt<ServiceOrderTaskDtoCreate>();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _repositoryManager.ServiceOrderTaskRepository.GetEntityById(id, false);
            if (service == null)
                throw new EntityNotFoundException(id, "ServiceOrderTask");
            _repositoryManager.ServiceOrderTaskRepository.DeleteEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceOrderTaskDto>> GetAllAsync(bool trackChanges)
        {
            var services = await _repositoryManager.ServiceOrderTaskRepository.GetAllEntity(trackChanges);
            var serviceDtos = services.Adapt<IEnumerable<ServiceOrderTaskDto>>();
            return serviceDtos;
        }

        public async Task<ServiceOrderTaskDto> GetByIdAsync(int id, bool trackChanges)
        {
            var task = await _repositoryManager.ServiceOrderTaskRepository.GetEntityById(id, trackChanges);
            if (task == null)
                throw new EntityNotFoundException(id, "ServiceOrderTask");
            var taskDto = task.Adapt<ServiceOrderTaskDto>();
            taskDto.Sowos=task.ServiceOrderWorkorders.Adapt<ICollection<ServiceOrderWorkorderDto>>().Select(c=>new ServiceOrderWorkorderDto
            {
                SowoId = c.SowoId,
                SowoModifiedDate = c.SowoModifiedDate,
                SowoName = c.SowoName,
                SowoStatus = c.SowoStatus,
            }).ToList();
            return taskDto;
        }

        public async Task<ServiceOrderTaskDtoCreate> UpdateAsync(int id, ServiceOrderTaskDtoCreate entity)
        {
            var services = await _repositoryManager.ServiceOrderTaskRepository.GetEntityById(id, true);
            if (services == null)
                throw new EntityNotFoundException(id, "ServiceOrderTask");

            services.SeotId = id;
            services.SeotName = entity.SeotName is not null? entity.SeotName: services.SeotName;
            services.SeotStartdate = entity.SeotStartdate is not null ? entity.SeotStartdate : services.SeotStartdate;
            services.SeotEnddate = entity.SeotEnddate is not null ? entity.SeotEnddate : services.SeotEnddate;
            services.SeotActualStartdate = entity.SeotActualStartdate is not null? entity.SeotActualStartdate : services.SeotActualStartdate;
            services.SeotActualEnddate = entity.SeotActualEnddate is not null ? entity.SeotActualEnddate : services.SeotActualEnddate;
            services.SeotStatus = entity.SeotStatus is not null ? entity.SeotStatus : services.SeotStatus;
            services.SeotArwgCode = entity.SeotArwgCode is not null? entity.SeotArwgCode: services.SeotArwgCode;
            services.SeotSeroId = entity.SeotSeroId is not null ? entity.SeotSeroId : services.SeotSeroId;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return services.Adapt<ServiceOrderTaskDtoCreate>();
        }
    }
}

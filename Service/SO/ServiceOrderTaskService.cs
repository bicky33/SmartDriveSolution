using Contract.DTO.SO;
using Domain.Exceptions;
using Domain.Repositories.SO;
using Mapster;
using Service.Abstraction.SO;

namespace ServiceOrderTask.SO
{
    public class ServiceOrderTaskService : IServiceSOEntityBase<ServiceOrderTaskDto,ServiceOrderTaskDtoCreate,int>
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
            var service = await _repositoryManager.ServiceOrderTaskRepository.GetEntityById(id,false);
            if (service == null)
                throw new EntityNotFoundException(id,"ServiceOrderTask");
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
            var service = await _repositoryManager.ServiceOrderTaskRepository.GetEntityById(id, trackChanges);
            if (service == null)
                throw new EntityNotFoundException(id,"ServiceOrderTask");
            var serviceDtos = service.Adapt<ServiceOrderTaskDto>();
            return serviceDtos;
        }

        public async Task<ServiceOrderTaskDtoCreate> UpdateAsync(int id, ServiceOrderTaskDtoCreate entity)
        {
            var services = await _repositoryManager.ServiceOrderTaskRepository.GetEntityById(id, true);
            if (services == null)
                throw new EntityNotFoundException(id,"ServiceOrderTask");

            services.SeotId = id;
            services.SeotName = entity.SeotName;
            services.SeotStartdate=entity.SeotStartdate;
            services.SeotEnddate=entity.SeotEnddate;
            services.SeotActualStartdate=entity.SeotActualStartdate;
            services.SeotActualEnddate=entity.SeotActualEnddate;
            services.SeotStatus=entity.SeotStatus.ToString();
            services.SeotArwgCode=entity.SeotArwgCode;
            services.SeotSeroId = entity.SeotSeroId;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return services.Adapt<ServiceOrderTaskDtoCreate>();
        }
    }
}

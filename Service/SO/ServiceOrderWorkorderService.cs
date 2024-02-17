using Contract.DTO.SO;
using Domain.Exceptions;
using Domain.Repositories.SO;
using Mapster;
using Service.Abstraction.SO;

namespace Service.SO
{
    public class ServiceOrderWorkorderService : IServiceSOEntityBase<ServiceOrderWorkorderDto, ServiceOrderWorkorderDtoCreate, int>
    {
        private readonly IRepositorySOManager _repositoryManager;

        public ServiceOrderWorkorderService(IRepositorySOManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ServiceOrderWorkorderDtoCreate> CreateAsync(ServiceOrderWorkorderDtoCreate entity)
        {
            var service = entity.Adapt<Domain.Entities.SO.ServiceOrderWorkorder>();
            _repositoryManager.ServiceOrderWorkorderRepository.CreateEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return service.Adapt<ServiceOrderWorkorderDtoCreate>();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _repositoryManager.ServiceOrderWorkorderRepository.GetEntityById(id, false);
            if (service == null)
                throw new EntityNotFoundException(id, "ServiceOrderTask");
            _repositoryManager.ServiceOrderWorkorderRepository.DeleteEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceOrderWorkorderDto>> GetAllAsync(bool trackChanges)
        {
            var services = await _repositoryManager.ServiceOrderWorkorderRepository.GetAllEntity(trackChanges);
            var serviceDtos = services.Adapt<IEnumerable<ServiceOrderWorkorderDto>>();
            return serviceDtos;
        }

        public async Task<ServiceOrderWorkorderDto> GetByIdAsync(int id, bool trackChanges)
        {
            var service = await _repositoryManager.ServiceOrderWorkorderRepository.GetEntityById(id, trackChanges);
            if (service == null)
                throw new EntityNotFoundException(id, "ServiceOrderTask");
            var serviceDtos = service.Adapt<ServiceOrderWorkorderDto>();
            return serviceDtos;
        }

        public async Task<ServiceOrderWorkorderDtoCreate> UpdateAsync(int id, ServiceOrderWorkorderDtoCreate entity)
        {
            var services = await _repositoryManager.ServiceOrderWorkorderRepository.GetEntityById(id, true);
            if (services == null)
                throw new EntityNotFoundException(id,"ServiceOrderTask");

            services.SowoId = id;
            services.SowoName = entity.SowoName;
            services.SowoModifiedDate= entity.SowoStatus != services.SowoStatus ? DateTime.Now : entity.SowoModifiedDate;
            services.SowoStatus=entity.SowoStatus;
            services.SowoSeotId=entity.SowoSeotId;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            
            return services.Adapt<ServiceOrderWorkorderDtoCreate>();
        }
    }
}

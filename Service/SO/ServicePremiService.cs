using Contract.DTO.SO;
using Domain.Exceptions;
using Domain.Repositories.SO;
using Mapster;
using Service.Abstraction.SO;

namespace Service.SO
{
    public class ServicePremiService : IServiceSOEntityBase<ServicePremiDto, ServicePremiDtoCreate, int>
    {
        private readonly IRepositorySOManager _repositoryManager;

        public ServicePremiService(IRepositorySOManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ServicePremiDtoCreate> CreateAsync(ServicePremiDtoCreate entity)
        {
            var service = entity.Adapt<Domain.Entities.SO.ServicePremi>();
            _repositoryManager.ServicePremiRepository.CreateEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return service.Adapt<ServicePremiDtoCreate>();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _repositoryManager.ServicePremiRepository.GetEntityById(id, false);
            if (service == null)
                throw new EntityNotFoundException(id, "Service Premi");
            _repositoryManager.ServicePremiRepository.DeleteEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServicePremiDto>> GetAllAsync(bool trackChanges)
        {
            var services = await _repositoryManager.ServicePremiRepository.GetAllEntity(trackChanges);
            if (services is null)
                return new List<ServicePremiDto>();
            else
            {
                var serviceDto = services.Adapt<IEnumerable<ServicePremiDto>>();
                return serviceDto;
            }
        }

        public async Task<ServicePremiDto> GetByIdAsync(int id, bool trackChanges)
        {
            var service = await _repositoryManager.ServicePremiRepository.GetEntityById(id, trackChanges);
            if (service == null)
                throw new EntityNotFoundException(id, "Service Premi");
            var serviceDtos = service.Adapt<ServicePremiDto>();
            return serviceDtos;
        }

        public async Task<ServicePremiDtoCreate> UpdateAsync(int id, ServicePremiDtoCreate entity)
        {
            var services = await _repositoryManager.ServicePremiRepository.GetEntityById(id, true);
            if (services == null)
                throw new EntityNotFoundException(id, "Service Premi");

            services.SemiServId = id;
            services.SemiPremiDebet = entity.SemiPremiDebet;
            services.SemiPremiCredit = entity.SemiPremiCredit;
            services.SemiPaidType = entity.SemiPaidType;
            services.SemiStatus = entity.SemiStatus;
            services.SemiModifiedDate = entity.SemiModifiedDate;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return services.Adapt<ServicePremiDtoCreate>();
        }
    }
}

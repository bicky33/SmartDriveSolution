using Contract.DTO.SO;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories.SO;
using Mapster;
using Service.Abstraction.SO;

namespace Service.SO
{
    public class ServiceService : IServiceSOEntityBase<ServiceDto,ServiceDtoCreate,int>
    {
        private readonly IRepositorySOManager _repositoryManager;

        public ServiceService(IRepositorySOManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ServiceDtoCreate> CreateAsync(ServiceDtoCreate entity)
        {
            var service = entity.Adapt<Domain.Entities.SO.Service>();
            _repositoryManager.ServiceRepository.CreateEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return service.Adapt<ServiceDtoCreate>();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _repositoryManager.ServiceRepository.GetEntityById(id,false);
            if (service == null)
                throw new EntityNotFoundException(id,"Service");
            _repositoryManager.ServiceRepository.DeleteEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceDto>> GetAllAsync(bool trackChanges)
        {
            var services = await _repositoryManager.ServiceRepository.GetAllEntity(trackChanges);
            var serviceDtos = services.Adapt<IEnumerable<ServiceDto>>();
            return serviceDtos;
        }

        public async Task<ServiceDto> GetByIdAsync(int id, bool trackChanges)
        {
            var service = await _repositoryManager.ServiceRepository.GetEntityById(id, trackChanges);
            if (service == null)
                throw new EntityNotFoundException(id,"Service");
            var serviceDtos = service.Adapt<ServiceDto>();
            return serviceDtos;
        }

        public async Task<ServiceDtoCreate> UpdateAsync(int id, ServiceDtoCreate entity)
        {
            var services = await _repositoryManager.ServiceRepository.GetEntityById(id, true);
            if (services == null)
                throw new EntityNotFoundException(id,"Service");

            services.ServId = id;
            services.ServCreatedOn = entity.ServCreatedOn;
            services.ServType= entity.ServType;
            services.ServInsuranceNo=entity.ServInsuranceNo;
            services.ServStatus=entity.ServStatus;
            services.ServVehicleNo=entity.ServVehicleNo;
            services.ServStartdate=entity.ServStartdate;
            services.ServEnddate=entity.ServEnddate;
            services.ServCustEntityid = entity.ServCustEntityid;
            services.ServCreqEntityid = entity.ServCreqEntityid;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return services.Adapt<ServiceDtoCreate>();
        }
    }
}

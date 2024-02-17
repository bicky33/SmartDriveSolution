using Contract.DTO.SO;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories.SO;
using Mapster;
using Service.Abstraction.SO;

namespace Service.SO
{
    public class ServicePremiCreditService : IServiceSOEntityBase<ServicePremiCreditDto,ServicePremiCreditDtoCreate,int>
    {
        private readonly IRepositorySOManager _repositoryManager;

        public ServicePremiCreditService(IRepositorySOManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ServicePremiCreditDtoCreate> CreateAsync(ServicePremiCreditDtoCreate entity)
        {
            var service = entity.Adapt<Domain.Entities.SO.ServicePremiCredit>();
            _repositoryManager.ServicePremiCreditRepository.CreateEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return service.Adapt<ServicePremiCreditDtoCreate>();
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _repositoryManager.ServicePremiCreditRepository.GetEntityById(id,false);
            if (service == null)
                throw new EntityNotFoundException(id,"Service Premi Credit");
            _repositoryManager.ServicePremiCreditRepository.DeleteEntity(service);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServicePremiCreditDto>> GetAllAsync(bool trackChanges)
        {
            var services = await _repositoryManager.ServicePremiCreditRepository.GetAllEntity(trackChanges);
            var serviceDtos = services.Adapt<IEnumerable<ServicePremiCreditDto>>();
            return serviceDtos;
        }

        public async Task<ServicePremiCreditDto> GetByIdAsync(int id, bool trackChanges)
        {
            var service = await _repositoryManager.ServicePremiCreditRepository.GetEntityById(id, trackChanges);
            if (service == null)
                throw new EntityNotFoundException(id,"Service Premi Credit");
            var serviceDtos = service.Adapt<ServicePremiCreditDto>();
            return serviceDtos;
        }

        public async Task<ServicePremiCreditDtoCreate> UpdateAsync(int id, ServicePremiCreditDtoCreate entity)
        {
            var services = await _repositoryManager.ServicePremiCreditRepository.GetEntityById(id, true);
            if (services == null)
                throw new EntityNotFoundException(id,"Service Premi Credit");

            services.SecrId = id;
            services.SecrServId = entity.SecrServId;
            services.SecrYear = entity.SecrYear;
            services.SecrPremiDebet = entity.SecrPremiDebet;
            services.SecrPremiCredit = entity.SecrPremiCredit;
            services.SecrTrxDate = entity.SecrTrxDate;
            services.SecrDuedate = entity.SecrDuedate;
            services.SecrPatrTrxno = entity.SecrPatrTrxno;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return services.Adapt<ServicePremiCreditDtoCreate>();
        }
    }
}

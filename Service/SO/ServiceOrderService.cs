using Contract.DTO.SO;
using Domain.Exceptions.SO;
using Domain.Repositories.SO;
using Mapster;
using Service.Abstraction.SO;

namespace Service.SO
{
    public class ServiceOrderService : IServiceSOEntityBase<ServiceOrderDto, ServiceOrderDtoCreate, string>
    {
        private readonly IRepositorySOManager _repositoryManager;

        public ServiceOrderService(IRepositorySOManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ServiceOrderDtoCreate> CreateAsync(ServiceOrderDtoCreate entity)
        {
            var serviceOrder = entity.Adapt<Domain.Entities.SO.ServiceOrder>();
            _repositoryManager.ServiceOrderRepository.CreateEntity(serviceOrder);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return serviceOrder.Adapt<ServiceOrderDtoCreate>();
        }

        public async Task DeleteAsync(string id)
        {
            var serviceOrder = await _repositoryManager.ServiceOrderRepository.GetEntityById(id, false);
            if (serviceOrder == null)
                throw new EntityNotFoundExceptionSO(id, "Service Order");
            _repositoryManager.ServiceOrderRepository.DeleteEntity(serviceOrder);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceOrderDto>> GetAllAsync(bool trackChanges)
        {
            var serviceOrder = await _repositoryManager.ServiceOrderRepository.GetAllEntity(trackChanges);
            var serviceOrderDtos = serviceOrder.Adapt<IEnumerable<ServiceOrderDto>>();
            return serviceOrderDtos;
        }

        public async Task<ServiceOrderDto> GetByIdAsync(string id, bool trackChanges)
        {
            var serviceOrder = await _repositoryManager.ServiceOrderRepository.GetEntityById(id, trackChanges);
            if (serviceOrder == null)
                throw new EntityNotFoundExceptionSO(id, "Service Order");

            var serviceOrderDtos = serviceOrder.Adapt<ServiceOrderDto>();
            // service order task
            serviceOrderDtos.Seots = serviceOrder.ServiceOrderTasks.Adapt<List<ServiceOrderTaskDto>>().Select(c => new ServiceOrderTaskDto
            {
                SeotId = c.SeotId,
                SeotName = c.SeotName,
                SeotStatus = c.SeotStatus,
                SeotStartdate = c.SeotStartdate,
                SeotEnddate = c.SeotEnddate,
                Sowos = c.Sowos,
            }).ToList();
            // service order workorder
            foreach (var seot in serviceOrder.ServiceOrderTasks.Select((value, index) => new { value, index }))
            {
                serviceOrderDtos.Seots[seot.index].Sowos = seot.value.ServiceOrderWorkorders.Adapt<ICollection<ServiceOrderWorkorderDto>>().Select(c => new ServiceOrderWorkorderDto
                {
                    SowoId = c.SowoId,
                    SowoModifiedDate = c.SowoModifiedDate,
                    SowoName = c.SowoName,
                    SowoStatus = c.SowoStatus,
                }).ToList();
            }
            return serviceOrderDtos;
        }

        public async Task<ServiceOrderDtoCreate> UpdateAsync(string id, ServiceOrderDtoCreate entity)
        {
            var serviceOrder = await _repositoryManager.ServiceOrderRepository.GetEntityById(id, true);
            if (serviceOrder == null)
                throw new EntityNotFoundExceptionSO(id, "Service Order");

            serviceOrder.SeroId = id;
            serviceOrder.SeroOrdtType = entity.SeroOrdtType is not null ? entity.SeroOrdtType : serviceOrder.SeroOrdtType;
            serviceOrder.SeroStatus = entity.SeroStatus is not null ? entity.SeroStatus : serviceOrder.SeroStatus;
            serviceOrder.SeroReason = entity.SeroReason is not null ? entity.SeroReason : serviceOrder.SeroReason;
            serviceOrder.ServClaimNo = entity.ServClaimNo is not null ? entity.ServClaimNo : serviceOrder.ServClaimNo;
            serviceOrder.ServClaimStartdate = entity.ServClaimStartdate is not null ? entity.ServClaimStartdate : serviceOrder.ServClaimStartdate;
            serviceOrder.ServClaimEnddate = entity.ServClaimEnddate is not null ? entity.ServClaimEnddate : serviceOrder.ServClaimEnddate;
            serviceOrder.SeroServId = entity.SeroServId is not null ? entity.SeroServId : serviceOrder.SeroServId;
            serviceOrder.SeroSeroId = entity.SeroSeroId is not null ? entity.SeroSeroId : serviceOrder.SeroSeroId;
            serviceOrder.SeroAgentEntityid = entity.SeroAgentEntityid is not null ? entity.SeroAgentEntityid : serviceOrder.SeroAgentEntityid;
            serviceOrder.SeroPartId = entity.SeroPartId is not null ? entity.SeroPartId : serviceOrder.SeroPartId;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return serviceOrder.Adapt<ServiceOrderDtoCreate>();
        }
    }
}

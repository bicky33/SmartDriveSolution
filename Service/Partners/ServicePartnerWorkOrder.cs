using Contract.DTO.Partners;
using Contract.Records;
using Domain.Entities.SO;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using Mapster;
using Service.Abstraction.Partners;

namespace Service.Partners
{
    public class ServicePartnerWorkOrder : IServicePartnerWorkOrder
    {
        private readonly IRepositoryPartnerManager _repositoryPartnerManager;

        public ServicePartnerWorkOrder(IRepositoryPartnerManager repositoryPartnerManager)
        {
            _repositoryPartnerManager = repositoryPartnerManager;
        }

        public async Task<IEnumerable<PartnerWorkOrderResponse>> GetAll(int seroPartId, string seotArwgCode)
        {
            IEnumerable<ServiceOrderWorkorder> data = await _repositoryPartnerManager.RepositoryPartnerWorkOrder.GetAllAsync(seroPartId, seotArwgCode);
            return data.Adapt<IEnumerable<PartnerWorkOrderResponse>>();
        }

        public async Task<PaginationDTO<PartnerWorkOrderResponse>> GetAllPaging(int seroPartId, string seotArwgCode, EntityParameter parameter)
        {
            PagedList<ServiceOrderWorkorder> workOrders = await _repositoryPartnerManager.RepositoryPartnerWorkOrder.GetAllAsyncPaging(seroPartId, seotArwgCode, parameter);
            var workOrderDTO = workOrders.Adapt<IEnumerable<PartnerWorkOrderResponse>>().ToList();
            PaginationDTO<PartnerWorkOrderResponse> pagination = new(workOrders.TotalPages, workOrders.CurrentPage, workOrderDTO);
            return pagination;
        }
    }
}

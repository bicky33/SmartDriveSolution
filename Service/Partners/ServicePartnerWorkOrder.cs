using Contract.DTO.Partners;
using Domain.Entities.SO;
using Domain.Repositories.Partners;
using Mapster;
using Service.Abstraction.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

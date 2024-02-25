using Domain.Entities.SO;
using Domain.Repositories.Partners;
using Service.Abstraction.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Partners
{
    public class ServicePartnerBatchInvoice : IServicePartnerBatchInvoice
    {
        private readonly IRepositoryPartnerManager _repositoryPartnerManager;

        public ServicePartnerBatchInvoice(IRepositoryPartnerManager repositoryPartnerManager)
        {
            _repositoryPartnerManager = repositoryPartnerManager;
        }

        public async Task CreateBatch()
        {
            IEnumerable<Domain.Entities.SO.Service> services = await _repositoryPartnerManager.RepositoryPartnerBatchInvoice.GetAllData();
        }

        public Task<IPartnerBatchInvoice> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

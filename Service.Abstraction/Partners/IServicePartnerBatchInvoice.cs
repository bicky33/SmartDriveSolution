using Contract.DTO.Partners;
using Domain.Repositories.Partners;
using Domain.RequestFeatured;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Partners
{
    public interface IServicePartnerBatchInvoice
    {
        Task<IEnumerable<PartnerBatchInvoiceResponse>> GetAll();
        Task CreateBatch();
        Task<IEnumerable<PartnerBatchInvoiceResponse>> GetAllPagingAsync(EntityParameter parameter);
    }
}

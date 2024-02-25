using Domain.Repositories.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Partners
{
    public interface IServicePartnerBatchInvoice
    {
        public Task<IPartnerBatchInvoice> GetAll();
        public Task CreateBatch();
    }
}

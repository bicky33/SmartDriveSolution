using Contract.DTO.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Partners
{
    public interface IServicePartnerWorkOrder
    {
        Task<IEnumerable<PartnerWorkOrderResponse>> GetAll(int seroPartId, string seotArwgCode);
    }
}

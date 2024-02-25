using Domain.Entities.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Partners
{
    public interface IRepositoryPartnerWorkOrder
    {
        Task<IEnumerable<ServiceOrderWorkorder>> GetAllAsync(int seroPartId, string seotArwgCode);
    }
}

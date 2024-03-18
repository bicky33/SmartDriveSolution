using Domain.Entities.SO;
using Domain.RequestFeatured;
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
        Task<PagedList<ServiceOrderWorkorder>> GetAllAsyncPaging(int seroPartId, string seotArwgCode, EntityParameter parameter);

    }
}

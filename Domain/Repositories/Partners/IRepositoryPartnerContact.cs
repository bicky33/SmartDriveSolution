using Domain.Entities.Partners;
using Domain.Repositories.Base;
using Domain.RequestFeatured;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Partners
{
    public interface IRepositoryPartnerContact: IRepositoryEntityBase<PartnerContact>
    {
        Task<PagedList<PartnerContact>> GetAllPagingAsync(bool trackChanges, EntityParameter parameter);
        Task<PartnerContact> GetEntityById(int pacoPatrnEntityid, int pacoUserEntityid, bool trackChanges);
    }
}

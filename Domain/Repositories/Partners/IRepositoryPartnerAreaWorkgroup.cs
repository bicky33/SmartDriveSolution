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
    public interface IRepositoryPartnerAreaWorkgroup: IRepositoryEntityBase<PartnerAreaWorkgroup>
    {
        Task<PartnerAreaWorkgroup> GetEntityById(bool trackChanges, int partnerId, int userId, string areaWorkgroupCode);
        Task<PagedList<PartnerAreaWorkgroup>> GetAllPaging(bool trackChanges, EntityParameter parameter);


    }
}

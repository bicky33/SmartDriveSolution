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
    public interface IRepositoryPartner: IRepositoryEntityBase<Partner>
    {
        Task<PagedList<Partner>> GetAllPaging(bool trackChanges, EntityParameter parameter);
    }
}

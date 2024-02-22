using Contract.DTO.Partners;
using Domain.RequestFeatured;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Partners
{
    public interface IServicePartner : IServiceEntityBase<PartnerDTO>
    {
        Task<IEnumerable<PartnerDTO>> GetAllPagingAsync(EntityParameter parameter, bool trackChanges);
    }
}

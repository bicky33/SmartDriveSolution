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
    public interface IServicePartnerContact
    {
        Task<IEnumerable<PartnerContactResponse>> GetAllPagingAsync(EntityParameter parameter);
        Task<PartnerContactDTO> GetByIdAsync(int pacoPatrnEntityid, int pacoUserEntityid, bool trackChanges);
        Task<IEnumerable<PartnerContactResponse>> GetAllAsync(bool trackChanges);
        Task<PartnerContactDTO> CreateAsync(PartnerDTO entity);
        Task UpdateAsync(int id, PartnerDTO entity);
        Task DeleteAsync(int pacoPatrnEntityid, int pacoUserEntityid);
    }
}

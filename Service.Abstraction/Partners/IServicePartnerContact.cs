using Contract.DTO.Partners;
using Contract.Records;
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
        Task<PaginationDTO<PartnerContactDTO>> GetAllPagingAsync(EntityParameter parameter);
        Task<PartnerContactDTO> GetByIdAsync(int pacoPatrnEntityid, int pacoUserEntityid, bool trackChanges);
        Task<IEnumerable<PartnerContactDTO>> GetAllAsync(bool trackChanges);
        Task<PartnerContactDTO> CreateAsync(PartnerContactDTO entity);
        Task UpdateAsync(int pacoPatrnEntityid, int pacoUserEntityid, PartnerContactDTO entity);
        Task DeleteAsync(int pacoPatrnEntityid, int pacoUserEntityid);
        Task<IEnumerable<PartnerContactDTO>> GetByUserId(int pacoUserEntityid, bool trackChanges);
    }
}

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
    public interface IServicePartnerAreaWorkgroup
    {
        Task<IEnumerable<PartnerAreaWorkgroupResponse>> GetAllPagingAsync(EntityParameter parameter, bool trackChanges);
        Task<IEnumerable<PartnerAreaWorkgroupResponse>> GetAllAsync(bool trackChanges);
        Task UpdateAsync(
            int PawoPatrEntityid,
            string PawoArwgCode,
            int PawoUserEntityid,
            PartnerAreaWorkgroupDTO entity,
            bool trackChanges
        );
        Task CreateAsync(PartnerAreaWorkgroupDTO entity);
        Task DeleteAsync(
            int PawoPatrEntityid,
            string PawoArwgCode,
            int PawoUserEntityid
        );

        Task<PartnerAreaWorkgroupDTO> GetByIdAsync(
            int PawoPatrEntityid,
            string PawoArwgCode,
            int PawoUserEntityid,
            bool trackChanges);
    }
}

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
    public interface IServicePartnerAreaWorkgroup
    {
        Task<PaginationDTO<PartnerAreaWorkgroupResponse>> GetAllPagingAsync(EntityParameter parameter, bool trackChanges);
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
        Task<IEnumerable<PartnerAreaWorkgroupDTO>> GetByPartnerAndUserId(int pawoUserId, int pawoPatrId, bool trackChanges);
    }
}

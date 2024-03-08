using Contract.DTO.SO;
using Service.Abstraction.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Partners
{
    public interface IServicePartnerClaimAssetSparepartBatch
    {
        Task CreateBatch(ClaimAssetSparepartRecords request);
        Task DeleteBatch(int caspPartEntityid, string caspSeroId);

        Task<IEnumerable<ClaimAssetSparepartDto>> GetByParameter(int caspPartEntityid, string caspSeroId);
    }
}

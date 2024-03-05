using Domain.Entities.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Partners
{
    public interface IRepositoryPartnerClaimAssetSparepartBatch
    {
        Task CreateBatch(IEnumerable<ClaimAssetSparepart> data);
        Task DeleteBatch(int CaspPartEntityid, string CaspSeroId);

        Task<IEnumerable<ClaimAssetSparepart>> GetByParameter(int CaspPartEntityid, string CaspSeroId);

    }
}

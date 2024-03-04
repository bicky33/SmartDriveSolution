using Domain.Entities.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Partners
{
    public interface IRepositoryPartnerClaimAssetEvidenceBatch
    {
        Task CreateBatch(IEnumerable<ClaimAssetEvidence> data);
        Task DeleteBatch(int CaspPartEntityid, string CaspSeroId);
        Task<IEnumerable<ClaimAssetEvidence>> GetData(int CaspPartEntityid, string CaspSeroId);

    }
}

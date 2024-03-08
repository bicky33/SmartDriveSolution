using Contract.DTO.SO;
using Domain.Entities.SO;
using Domain.Repositories.Partners;
using Mapster;
using Service.Abstraction.Partners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Partners
{
    public class ServicePartnerClaimAssetSparepartBatch : IServicePartnerClaimAssetSparepartBatch
    {
        private readonly IRepositoryPartnerManager _repositoryPartnerManager;

        public ServicePartnerClaimAssetSparepartBatch(IRepositoryPartnerManager repositoryPartnerManager)
        {
            _repositoryPartnerManager = repositoryPartnerManager;
        }

        public async Task CreateBatch(List<ClaimAssetSparepartDtoCreate> request)
        {
            IEnumerable<ClaimAssetSparepart>  claimAssetSpareparts = request.Adapt<IEnumerable<ClaimAssetSparepart>>();
            await _repositoryPartnerManager.RepositoryClaimAssetSparepartBatch.CreateBatch(claimAssetSpareparts);
        }

        public async Task DeleteBatch(int caspPartEntityid, string caspSeroId)
        {
            await _repositoryPartnerManager.RepositoryClaimAssetSparepartBatch.DeleteBatch(caspPartEntityid, caspSeroId);
        }

        public async Task<IEnumerable<ClaimAssetSparepartDto>> GetByParameter(int caspPartEntityid, string caspSeroId)
        {
            IEnumerable<ClaimAssetSparepart> claims = await _repositoryPartnerManager.RepositoryClaimAssetSparepartBatch.GetByParameter(caspPartEntityid, caspSeroId);
            return claims.Adapt<IEnumerable<ClaimAssetSparepartDto>>();
        }
    }
}

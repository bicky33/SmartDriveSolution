using Contract.DTO.SO;
using Domain.Entities.SO;
using Domain.Enum;
using Domain.Repositories.Partners;
using Domain.Repositories.SO;
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
        private readonly IRepositorySOManager _repositorySOManager;

        public ServicePartnerClaimAssetSparepartBatch(IRepositoryPartnerManager repositoryPartnerManager, IRepositorySOManager repositorySOManager)
        {
            _repositoryPartnerManager = repositoryPartnerManager;
            _repositorySOManager = repositorySOManager;
        }

        public async Task CreateBatch(ClaimAssetSparepartRecords request)
        {
            IEnumerable<ClaimAssetSparepart>  claimAssetSpareparts = request.Data.Adapt<IEnumerable<ClaimAssetSparepart>>();
            await _repositoryPartnerManager.RepositoryClaimAssetSparepartBatch.CreateBatch(claimAssetSpareparts);
            ServiceOrderWorkorder sowo = await _repositorySOManager.ServiceOrderWorkorderRepository.GetEntityById(request.SowoId, true);
            sowo.SowoStatus = nameof(EnumModuleServiceOrder.SEOTSTATUS.COMPLETED);
            await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
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

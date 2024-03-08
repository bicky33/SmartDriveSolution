using Contract.DTO.Partners;
using Contract.DTO.SO;
using Service.Abstraction.SO;

namespace Service.Abstraction.Partners
{
    public interface IServicePartnerClaimAssetEvidence: IServiceSOEntityBase<ClaimAssetEvidenceDto, ClaimAssetEvidenceDtoCreate, int>
    {
        Task CreateAsyncWithLink(ClaimAssetEvidenceDtoCreate entityDto, string baseUrl);
        Task CreateBatch(PartnerClaimAssetEvidenceBatchRequest request, string baseUrl, int sowoId);
        Task DeleteBatch(int caspPartEntityid, string caspSeroId);

        Task<IEnumerable<ClaimAssetEvidenceDto>> GetByParameter(int caspPartEntityid, string caspSeroId);

    }
}

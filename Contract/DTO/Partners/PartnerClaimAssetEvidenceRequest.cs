using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.Partners
{
    public record PartnerClaimAssetEvidenceBatchRequest(
        [Required(ErrorMessage = "CaevNote cannot be null or empty.")]
        List<string> CaevNote,

        [Required(ErrorMessage = "CaevFee cannot be null or empty.")]
        List<decimal> CaevFee,

        [Required(ErrorMessage = "CaevPartEntityid cannot be null or empty.")]
        List<int> CaevPartEntityid,

        [Required(ErrorMessage = "CaevSeroId cannot be null or empty.")]
        List<string> CaevSeroId,

        [Required(ErrorMessage = "Photo cannot be null or empty.")]
        List<IFormFile> Photo
    );

    public record PartnerClaimAssetEvidenceRequest(
        string CaevNote,
        decimal CaevFee,
        int CaevPartEntityid,
        string CaevSeroId,
        IFormFile Photo
    );
}

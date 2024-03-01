using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Contract.DTO.SO
{
    public record ClaimAssetEvidenceDtoCreate(
        int? CaevId,
        string? CaevFilename,
        int? CaevFilesize,
        string? CaevFiletype,
        string? CaevUrl,
        string? CaevNote,
        int? CaevPartEntityid,
        string? CaevSeroId,
        decimal? CaevServiceFee,
        DateTime? CaevCreatedDate,
        IFormFile Photo
    );
}

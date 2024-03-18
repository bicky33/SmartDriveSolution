namespace Contract.DTO.SO
{
    public record ClaimAssetEvidenceDto(
       int CaevId,
       string? CaevFilename,
       int? CaevFilesize,
       string? CaevFiletype,
       string? CaevUrl,
       string? CaevNote,
       int? CaevPartEntityid,
       string? CaevSeroId,
       decimal? CaevServiceFee,
       DateTime? CaevCreatedDate
    );
}

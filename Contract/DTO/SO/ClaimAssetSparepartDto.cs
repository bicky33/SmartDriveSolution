using Contract.DTO.Partners;

namespace Contract.DTO.SO
{
    public record ClaimAssetSparepartDto(
       int CaspId,
       string? CaspItemName,
       int? CaspQuantity,
       decimal? CaspItemPrice,
       decimal? CaspSubtotal,
       int? CaspPartEntityid,
       string? CaspSeroId,
       DateTime? CaspCreatedDate,
       PartnerDTO? CaspPartEntity,
       ServiceOrderDto? CaspSero
    );
}

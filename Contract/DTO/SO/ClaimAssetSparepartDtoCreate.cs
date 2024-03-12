using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.SO
{
    public record ClaimAssetSparepartDtoCreate(
        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid CaspId.")]
        int? CaspId,

        [Required(ErrorMessage = "CaspItemName is required.")]
        string? CaspItemName,

        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid CaspQuantity.")]
        int? CaspQuantity,

        [Range(0.01, double.MaxValue, ErrorMessage = "Please provide a valid CaspItemPrice.")]
        decimal? CaspItemPrice,

        [Range(0.01, double.MaxValue, ErrorMessage = "Please provide a valid CaspSubtotal.")]
        decimal? CaspSubtotal,

        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid CaspPartEntityid.")]
        int? CaspPartEntityid,

        string? CaspSeroId,

        DateTime? CaspCreatedDate
     );

}

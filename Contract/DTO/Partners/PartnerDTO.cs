using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Partners
{
    public record PartnerDTO(
        int PartEntityid,

        [Required(ErrorMessage = "Name is required.")]
        string? PartName,

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(255, ErrorMessage = "address must not exceed 255 characters")]
        string? PartAddress,

        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [Required(ErrorMessage = "PartJoinDate is required")]
        DateTime? PartJoinDate,

        [RegularExpression(@"^\d{1,35}$", ErrorMessage = "Invalid Account Numbers format. NPWP must be between 1 and 25 digits.")]
        [Required(ErrorMessage = "PartAccount is required")]
        string PartAccountNo,

        [RegularExpression(@"^\d{1,25}$", ErrorMessage = "Invalid NPWP format. NPWP must be between 1 and 25 digits.")]
        [Required(ErrorMessage = "PartNpwp is required")]
        string PartNpwp,

        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid city ID.")]
        int PartCityId,

        string? CityName,

        [EnumDataType(typeof(PartnerStatus), ErrorMessage = "Invalid partner status.")]
        PartnerStatus PartStatus
    );
}

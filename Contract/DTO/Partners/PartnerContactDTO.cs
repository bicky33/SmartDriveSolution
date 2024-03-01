using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Partners
{
    public record PartnerContactDTO(
        [Range(1, int.MaxValue, ErrorMessage = "Partner entity ID must be greater than zero.")]
        int PacoPatrnEntityid,

        int PacoUserEntityid,

        [EnumDataType(typeof(PartnerStatus), ErrorMessage = "Invalid partner status.")]
        PartnerStatus PacoStatus,

        [Required(ErrorMessage = "Full name is required.")]
        string? FullName,

        [RegularExpression(@"^\+?\d{0,15}$", ErrorMessage = "Invalid phone number format.")]
        string? PhoneNumber,

        string? PacoPatrnEntityName,

        bool IsGranted
    );
}

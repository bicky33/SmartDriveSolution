using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Partners
{
    public record PartnerAreaWorkgroupDTO(
        [Range(1, int.MaxValue, ErrorMessage = "Partner entity ID must be greater than zero.")]
        int PawoPatrEntityid,

        [Required(ErrorMessage = "Area Workgroup Code is required.")]
        string PawoArwgCode,

        [Range(1, int.MaxValue, ErrorMessage = "User entity ID must be greater than zero.")]
        int PawoUserEntityid,

        [EnumDataType(typeof(PartnerStatus), ErrorMessage = "Invalid partner status.")]
        PartnerStatus PawoStatus
    );
}

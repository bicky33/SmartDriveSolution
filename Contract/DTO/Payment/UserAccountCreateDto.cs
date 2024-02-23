using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contract.DTO.Payment
{
    public class UserAccountCreateDto
    {
        [StringLength(30)]
        [Required]
        public string UsacAccountno { get; set; }
        [JsonIgnore]
        public AccountTypeEnum? UsacType { get; set; }
        public int? UsacBankEntityid { get; set; } = null;
        public int? UsacFintEntityid { get; set; } = null;
        public int? UsacUserEntityid { get; set; }
    }

}

using Contract.Attributes;
using Contract.DTO.Master;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contract.DTO.Payment
{
    public class UserAccountDto
    {
        [JsonIgnore]
        public int UsacId { get; set; }
        [StringLength(30)]
        [Required]
        public string UsacAccountno { get; set; }
        public decimal? UsacDebet { get; set; }
        public decimal? UsacCredit { get; set; }
        [StringLength(15)] 
        public string? UsacType { get; set; }
        public int? UsacBankEntityid { get; set; } = null;
        public int? UsacFintEntityid { get; set; } = null;
        public int? UsacUserEntityid { get; set; }
        public BankDto? UsacBankEntity { get; set; }
        public FintechDto? UsacFintEntity { get; set; }
    }

}

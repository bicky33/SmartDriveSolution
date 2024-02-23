using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contract.DTO.Payment
{
    public class PaymentTransactionCreateDto
    {
        [Required]
        [Range(50000, double.MaxValue)]
        public decimal? SendAmount { get; set; }
        [Required]
        public string? PatrUsacAccountNoFrom { get; set; }
        [Required]
        public string? PatrUsacAccountNoTo { get; set; }
        [Required]
        public PaymentTypeEnum? PatrType { get; set; }
        public string? PatrNotes { get; set; }
    }

    public class PaymentTransactionDepositDto
    {
        [Required]
        [Range(50000, double.MaxValue)]
        public decimal? SendAmount { get; set; }
        [Required]
        public string? PatrUsacAccountNoTo { get; set; }
        [JsonIgnore]
        public PaymentTypeEnum? PatrType { get; set; } = PaymentTypeEnum.DEPOSIT;
        public string? PatrNotes { get; set; }
    }
}

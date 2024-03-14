using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.SO
{
    public class ServicePremiDto
    {
        [Required]
        public int SemiServId { get; init; }

        public decimal? SemiPremiDebet { get; init; }

        public decimal? SemiPremiCredit { get; init; }

        [StringLength(15)]
        public string? SemiPaidType { get; init; }

        [StringLength(15)]
        public string? SemiStatus { get; init; }

        public DateTime? SemiModifiedDate { get; init; }
    }
}

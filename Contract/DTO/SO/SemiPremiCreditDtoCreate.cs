using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.SO
{
    public class ServicePremiCreditDtoCreate
    {
        public int? SecrId { get; init; }
        [Required]
        public int SecrServId { get; init; }
        [StringLength(4)]
        public string? SecrYear { get; init; }

        public decimal? SecrPremiDebet { get; init; }

        public decimal? SecrPremiCredit { get; init; }

        public DateTime? SecrTrxDate { get; init; }

        public DateTime? SecrDuedate { get; init; }
        [StringLength(55)]
        public string? SecrPatrTrxno { get; init; }

    }
}

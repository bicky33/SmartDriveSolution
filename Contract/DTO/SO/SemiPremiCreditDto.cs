using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Payment;

namespace Contract.DTO.SO
{
    public class ServicePremiCreditDto
    {
        [Required]
        public int SecrId { get; init; }
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

        public PaymentTransaction? SecrPatrTrxnoNavigation { get; set; }
    }
}

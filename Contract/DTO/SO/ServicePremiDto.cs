using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.SO
{
    public class ServicePremiDto
    {
        [Required]
        public int SemiServId { get; init; }

        public decimal? SemiPremiDebet { get; init; }

        public decimal? SemiPremiCredit { get; init; }

        public string? SemiPaidType { get; init; }

        public string? SemiStatus { get; init; }

        public DateTime? SemiModifiedDate { get; init; }
    }
}

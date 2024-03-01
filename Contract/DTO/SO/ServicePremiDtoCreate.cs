using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.SO
{
    public class ServicePremiDtoCreate
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

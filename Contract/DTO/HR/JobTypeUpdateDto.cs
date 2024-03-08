using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR
{
    public class JobTypeUpdateDto
    {

        [Column("job_desc")]
        [StringLength(50)]

        public string? JobDesc { get; set; }

        [Column("job_rate_min", TypeName = "decimal(18, 0)")]
        public decimal? JobRateMin { get; set; }

        [Column("job_rate_max", TypeName = "decimal(18, 0)")]
        public decimal? JobRateMax { get; set; }
    }
}

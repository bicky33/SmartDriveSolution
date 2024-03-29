﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR
{
    public class JobTypeDtoCreate
    {
        [Key]
        [Column("job_code")]
        [StringLength(15)]

        public string JobCode { get; set; } = null!;

        [Column("job_modified_date", TypeName = "datetime")]
        public DateTime? JobModifiedDate { get; set; }

        [Column("job_desc")]
        [StringLength(50)]

        public string? JobDesc { get; set; }

        [Column("job_rate_min", TypeName = "decimal(18, 0)")]
        public decimal? JobRateMin { get; set; }

        [Column("job_rate_max", TypeName = "decimal(18, 0)")]
        public decimal? JobRateMax { get; set; }
    }
}

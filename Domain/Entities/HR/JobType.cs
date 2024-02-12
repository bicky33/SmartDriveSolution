using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.HR;

[Table("job_type", Schema = "hr")]
public partial class JobType
{
    [Key]
    [Column("job_code")]
    [StringLength(15)]
    [Unicode(false)]
    public string JobCode { get; set; } = null!;

    [Column("job_modified_date", TypeName = "datetime")]
    public DateTime? JobModifiedDate { get; set; }

    [Column("job_desc")]
    [StringLength(50)]
    [Unicode(false)]
    public string? JobDesc { get; set; }

    [Column("job_rate_min", TypeName = "decimal(18, 0)")]
    public decimal? JobRateMin { get; set; }

    [Column("job_rate_max", TypeName = "decimal(18, 0)")]
    public decimal? JobRateMax { get; set; }

    [InverseProperty("EmpJobCodeNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

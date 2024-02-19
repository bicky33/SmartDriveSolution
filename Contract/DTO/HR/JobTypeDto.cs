using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.HR;

namespace Contract.DTO.HR
{
    public class JobTypeDto
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

        //[InverseProperty("EmpJobCodeNavigation")]
        //public virtual ICollection<EmployeeUserDto> EmployeeUserDto { get; set; } = new List<EmployeeUserDto>();
    }
}

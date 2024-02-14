using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.HR;

[PrimaryKey("EmsaId", "EmsaEmpEntityid", "EmsaCreateDate")]
[Table("employee_salary_detail", Schema = "hr")]
public partial class EmployeeSalaryDetail
{
    [Key]
    [Column("emsa_id")]
    public int EmsaId { get; set; }

    [Key]
    [Column("emsa_emp_entityid")]
    public int EmsaEmpEntityid { get; set; }

    [Key]
    [Column("emsa_create_date", TypeName = "date")]
    public DateTime EmsaCreateDate { get; set; }

    [Column("emsa_name")]
    [StringLength(55)]
    [Unicode(false)]
    public string? EmsaName { get; set; }

    [Column("emsa_subtotal", TypeName = "money")]
    public decimal? EmsaSubtotal { get; set; }

    [ForeignKey("EmsaEmpEntityid")]
    [InverseProperty("EmployeeSalaryDetails")]
    public virtual Employee EmsaEmpEntity { get; set; } = null!;
}
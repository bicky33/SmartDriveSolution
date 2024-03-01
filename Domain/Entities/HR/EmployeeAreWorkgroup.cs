using Domain.Entities.CR;
using Domain.Entities.Master;
using Domain.Entities.SO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.HR;

[PrimaryKey("EawgEntityid", "EawgId")]
[Table("employee_are_workgroup", Schema = "hr")]
[Index("EawgId", Name = "UQ__employee__C54750D66DC8B49A", IsUnique = true)]
public partial class EmployeeAreWorkgroup
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    [Column("eawg_id")]
    public int EawgId { get; set; }

    [Key]
    [Column("eawg_entityid")]
    public int EawgEntityid { get; set; }

    [Column("eawg_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? EawgStatus { get; set; }

    [Column("eawg_arwg_code")]
    [StringLength(15)]
    [Unicode(false)]
    public string? EawgArwgCode { get; set; }

    [Column("eawg_modified_date", TypeName = "datetime")]
    public DateTime? EawgModifiedDate { get; set; }

    public virtual ICollection<CustomerRequest> CustomerRequests { get; set; } = new List<CustomerRequest>();

    [ForeignKey("EawgArwgCode")]
    [InverseProperty("EmployeeAreWorkgroups")]
    public virtual AreaWorkgroup? EawgArwgCodeNavigation { get; set; }

    [ForeignKey("EawgEntityid")]
    [InverseProperty("EmployeeAreWorkgroups")]
    public virtual Employee EawgEntity { get; set; } = null!;

    public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();

    [Column("soft_delete")]
    public string? SoftDelete { get; set; }
}
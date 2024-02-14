using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Payment;

[Table("fintech", Schema = "payment")]
[Index("FintName", Name = "UQ__fintech__96EC42741428C8E7", IsUnique = true)]
public partial class Fintech
{
    [Key]
    [Column("fint_entityid")]
    public int FintEntityid { get; set; }

    [Column("fint_name")]
    [StringLength(5)]
    [Unicode(false)]
    public string? FintName { get; set; }

    [Column("fint_desc")]
    [StringLength(55)]
    [Unicode(false)]
    public string? FintDesc { get; set; }

    [ForeignKey("FintEntityid")]
    [InverseProperty("Fintech")]
    public virtual BusinessEntity FintEntity { get; set; } = null!;

    [InverseProperty("UsacFintEntity")]
    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
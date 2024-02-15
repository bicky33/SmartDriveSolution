using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Payment;

[Table("user_accounts", Schema = "payment")]
[Index("UsacAccountno", Name = "UQ__user_acc__87A4C64BACB826BC", IsUnique = true)]
public partial class UserAccount
{
    [Key]
    [Column("usac_id")]
    public int UsacId { get; set; }

    [Column("usac_accountno")]
    [StringLength(30)]
    [Unicode(false)]
    public string? UsacAccountno { get; set; }

    [Column("usac_debet", TypeName = "money")]
    public decimal? UsacDebet { get; set; }

    [Column("usac_credit", TypeName = "money")]
    public decimal? UsacCredit { get; set; }

    [Column("usac_type")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UsacType { get; set; }

    [Column("usac_bank_entityid")]
    public int? UsacBankEntityid { get; set; }

    [Column("usac_fint_entityid")]
    public int? UsacFintEntityid { get; set; }

    [Column("usac_user_entityid")]
    public int? UsacUserEntityid { get; set; }

    [ForeignKey("UsacBankEntityid")]
    [InverseProperty("UserAccounts")]
    public virtual Bank? UsacBankEntity { get; set; }

    [ForeignKey("UsacFintEntityid")]
    [InverseProperty("UserAccounts")]
    public virtual Fintech? UsacFintEntity { get; set; }

    [ForeignKey("UsacUserEntityid")]
    [InverseProperty("UserAccounts")]
    public virtual User? UsacUserEntity { get; set; }
}
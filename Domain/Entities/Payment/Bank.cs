using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Payment;

[Table("banks", Schema = "payment")]
[Index("BankName", Name = "UQ__banks__AEBE0980F1FF603D", IsUnique = true)]
public partial class Bank
{
    [Key]
    [Column("bank_entityid")]
    public int BankEntityid { get; set; }

    [Column("bank_name")]
    [StringLength(5)]
    [Unicode(false)]
    public string? BankName { get; set; }

    [Column("bank_desc")]
    [StringLength(55)]
    [Unicode(false)]
    public string? BankDesc { get; set; }

    [ForeignKey("BankEntityid")]
    [InverseProperty("Bank")]
    public virtual BusinessEntity BankEntity { get; set; } = null!;

    [InverseProperty("UsacBankEntity")]
    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}

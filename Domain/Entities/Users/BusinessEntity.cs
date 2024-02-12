using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.CR;
using Domain.Entities.Partners;
using Domain.Entities.Payment;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Users;

[Table("business_entity", Schema = "users")]
public partial class BusinessEntity
{
    [Key]
    [Column("entityid")]
    public int Entityid { get; set; }

    [Column("entity_modified_date", TypeName = "datetime")]
    public DateTime EntityModifiedDate { get; set; }

    [InverseProperty("BankEntity")]
    public virtual Bank? Bank { get; set; }

    [InverseProperty("CreqEntity")]
    public virtual CustomerRequest? CustomerRequest { get; set; }

    [InverseProperty("FintEntity")]
    public virtual Fintech? Fintech { get; set; }

    [InverseProperty("PartEntity")]
    public virtual Partner? Partner { get; set; }

    [InverseProperty("UserEntity")]
    public virtual User? User { get; set; }
}

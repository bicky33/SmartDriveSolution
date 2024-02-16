using Domain.Entities.CR;
using Domain.Entities.HR;
using Domain.Entities.Partners;
using Domain.Entities.Payment;
using Domain.Entities.SO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Users;

[Table("users", Schema = "users")]
[Index("UserNpwp", Name = "UQ__users__58883F8602CC93E4", IsUnique = true)]
[Index("UserNationalId", Name = "UQ__users__60A5BA8FD8A27B24", IsUnique = true)]
[Index("UserName", Name = "UQ__users__7C9273C449437A87", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("user_entityid")]
    public int UserEntityid { get; set; }

    [Column("user_name")]
    [StringLength(15)]
    [Unicode(false)]
    public string? UserName { get; set; }

    [Column("user_password")]
    [StringLength(256)]
    [Unicode(false)]
    public string? UserPassword { get; set; }

    [Column("user_full_name")]
    [StringLength(85)]
    [Unicode(false)]
    public string? UserFullName { get; set; }

    [Column("user_email")]
    [StringLength(25)]
    [Unicode(false)]
    public string UserEmail { get; set; } = null!;

    [Column("user_birth_place")]
    [StringLength(55)]
    [Unicode(false)]
    public string? UserBirthPlace { get; set; }

    [Column("user_birth_date", TypeName = "datetime")]
    public DateTime? UserBirthDate { get; set; }

    [Column("user_national_id")]
    [StringLength(20)]
    [Unicode(false)]
    public string UserNationalId { get; set; } = null!;

    [Column("user_npwp")]
    [StringLength(35)]
    [Unicode(false)]
    public string? UserNpwp { get; set; }

    [Column("user_photo")]
    [StringLength(256)]
    [Unicode(false)]
    public string? UserPhoto { get; set; }

    [Column("user_modified_date", TypeName = "datetime")]
    public DateTime? UserModifiedDate { get; set; }

    [InverseProperty("CreqCustEntity")]
    public virtual ICollection<CustomerRequest> CustomerRequests { get; set; } = new List<CustomerRequest>();

    [InverseProperty("EmpEntity")]
    public virtual Employee? Employee { get; set; }

    [InverseProperty("PacoUserEntity")]
    public virtual ICollection<PartnerContact> PartnerContacts { get; set; } = new List<PartnerContact>();

    [InverseProperty("RetoUser")]
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    [InverseProperty("ServCustEntity")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    [InverseProperty("UsacUserEntity")]
    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();

    [InverseProperty("UsdrEntity")]
    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    [ForeignKey("UserEntityid")]
    [InverseProperty("User")]
    public virtual BusinessEntity UserEntity { get; set; } = null!;

    [InverseProperty("UsphEntity")]
    public virtual ICollection<UserPhone> UserPhones { get; set; } = new List<UserPhone>();

    [InverseProperty("UsroEntity")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
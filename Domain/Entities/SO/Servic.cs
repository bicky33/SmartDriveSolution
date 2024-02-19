using Domain.Entities.CR;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SO;

[Table("services", Schema = "so")]
public partial class Servic
{
    [Key]
    [Column("serv_id")]
    public int ServId { get; set; }

    [Column("serv_created_on", TypeName = "datetime")]
    public DateTime? ServCreatedOn { get; set; }

    [Column("serv_type")]
    [StringLength(15)]
    [Unicode(false)]
    public string? ServType { get; set; }

    [Column("serv_insuranceNo")]
    [StringLength(12)]
    [Unicode(false)]
    public string? ServInsuranceNo { get; set; }

    [Column("serv_vehicleNo")]
    [StringLength(12)]
    [Unicode(false)]
    public string? ServVehicleNo { get; set; }

    [Column("serv_startdate", TypeName = "datetime")]
    public DateTime? ServStartdate { get; set; }

    [Column("serv_enddate", TypeName = "datetime")]
    public DateTime? ServEnddate { get; set; }

    [Column("serv_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? ServStatus { get; set; }

    [Column("serv_serv_id")]
    public int? ServServId { get; set; }

    [Column("serv_cust_entityid")]
    public int? ServCustEntityid { get; set; }

    [Column("serv_creq_entityid")]
    public int? ServCreqEntityid { get; set; }

    [InverseProperty("ServServ")]
    public virtual ICollection<Servic> InverseServServ { get; set; } = new List<Servic>();

    [ForeignKey("ServCreqEntityid")]
    [InverseProperty("Services")]
    public virtual CustomerRequest? ServCreqEntity { get; set; }

    [ForeignKey("ServCustEntityid")]
    [InverseProperty("Services")]
    public virtual User? ServCustEntity { get; set; }

    [ForeignKey("ServServId")]
    [InverseProperty("InverseServServ")]
    public virtual Servic? ServServ { get; set; }

    [InverseProperty("SeroServ")]
    public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();

    [InverseProperty("SemiServ")]
    public virtual ServicePremi? ServicePremi { get; set; }

    [InverseProperty("SecrServ")]
    public virtual ICollection<ServicePremiCredit> ServicePremiCredits { get; set; } = new List<ServicePremiCredit>();
}
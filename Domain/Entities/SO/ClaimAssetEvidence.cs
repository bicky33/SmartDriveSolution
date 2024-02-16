using Domain.Entities.Partners;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.SO;

[Table("claim_asset_evidence", Schema = "so")]
public partial class ClaimAssetEvidence
{
    [Key]
    [Column("caev_id")]
    public int CaevId { get; set; }

    [Column("caev_filename")]
    [StringLength(55)]
    [Unicode(false)]
    public string? CaevFilename { get; set; }

    [Column("caev_filesize")]
    public int? CaevFilesize { get; set; }

    [Column("caev_filetype")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CaevFiletype { get; set; }

    [Column("caev_url")]
    [StringLength(255)]
    [Unicode(false)]
    public string? CaevUrl { get; set; }

    [Column("caev_note")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CaevNote { get; set; }

    [Column("caev_part_entityid")]
    public int? CaevPartEntityid { get; set; }

    [Column("caev_sero_id")]
    [StringLength(25)]
    [Unicode(false)]
    public string? CaevSeroId { get; set; }

    [Column("caev_service_fee", TypeName = "money")]
    public decimal? CaevServiceFee { get; set; }

    [Column("caev_created_date", TypeName = "datetime")]
    public DateTime? CaevCreatedDate { get; set; }

    [ForeignKey("CaevPartEntityid")]
    [InverseProperty("ClaimAssetEvidences")]
    public virtual Partner? CaevPartEntity { get; set; }

    [ForeignKey("CaevSeroId")]
    [InverseProperty("ClaimAssetEvidences")]
    public virtual ServiceOrder? CaevSero { get; set; }
}
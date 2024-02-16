using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.CR;

[PrimaryKey("CadocId", "CadocCreqEntityid")]
[Table("customer_insc_doc", Schema = "customer")]
public partial class CustomerInscDoc
{
    [Key]
    [Column("cadoc_id")]
    public int CadocId { get; set; }

    [Key]
    [Column("cadoc_creq_entityid")]
    public int CadocCreqEntityid { get; set; }

    [Column("cadoc_filename")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CadocFilename { get; set; }

    [Column("cadoc_filetype")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CadocFiletype { get; set; }

    [Column("cadoc_filesize")]
    public int? CadocFilesize { get; set; }

    [Column("cadoc_category")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CadocCategory { get; set; }

    [Column("cadoc_modified_date", TypeName = "datetime")]
    public DateTime? CadocModifiedDate { get; set; }

    [ForeignKey("CadocCreqEntityid")]
    [InverseProperty("CustomerInscDocs")]
    public virtual CustomerInscAsset CadocCreqEntity { get; set; } = null!;
}
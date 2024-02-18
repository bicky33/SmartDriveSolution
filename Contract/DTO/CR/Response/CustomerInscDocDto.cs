using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CR;

namespace Contract.DTO.CR.Response
{
    public class CustomerInscDocDto
    {
        public int CadocId { get; set; }
        public int CadocCreqEntityid { get; set; }
        public string? CadocFilename { get; set; }
        public string? CadocFiletype { get; set; }
        public int? CadocFilesize { get; set; }
        public string? CadocCategory { get; set; }
        public DateTime? CadocModifiedDate { get; set; }
        public CustomerInscAsset CadocCreqEntity { get; set; } = null!;
    }
}

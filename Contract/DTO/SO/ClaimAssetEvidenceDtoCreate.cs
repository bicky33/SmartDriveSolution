using Domain.Entities.Partners;
using Domain.Entities.SO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Contract.DTO.SO
{
    public class ClaimAssetEvidenceDtoCreate
    {
        public int? CaevId { get; init; }

        public string? CaevFilename { get; init; }

        public int? CaevFilesize { get; init; }

        public string? CaevFiletype { get; init; }

        public string? CaevUrl { get; init; }

        [StringLength(15)]
        public string? CaevNote { get; init; }

        public int? CaevPartEntityid { get; init; }

        public string? CaevSeroId { get; init; }

        public decimal? CaevServiceFee { get; init; }

        public DateTime? CaevCreatedDate { get; init; }

        [Required]
        public IFormFile Photo { get; set; }


    }
}

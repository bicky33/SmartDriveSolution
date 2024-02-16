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

namespace Contract.DTO.SO
{
    public class ClaimAssetEvidenceDto
    {
        [Required]
        public int CaevId { get; init; }

        public string? CaevFilename { get; init; }

        public int? CaevFilesize { get; init; }

        public string? CaevFiletype { get; init; }

        public string? CaevUrl { get; init; }

        public string? CaevNote { get; init; }

        public int? CaevPartEntityid { get; init; }

        public string? CaevSeroId { get; init; }

        public decimal? CaevServiceFee { get; init; }

        public DateTime? CaevCreatedDate { get; init; }

        //public Partner? CaevPartEntity { get; set; }

        public ServiceOrderDto? CaevSero { get; set; }

    }
}

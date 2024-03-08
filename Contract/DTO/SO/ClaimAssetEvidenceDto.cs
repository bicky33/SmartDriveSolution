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
    public record ClaimAssetEvidenceDto(
       int CaevId,
       string? CaevFilename,
       int? CaevFilesize,
       string? CaevFiletype,
       string? CaevUrl,
       string? CaevNote,
       int? CaevPartEntityid,
       string? CaevSeroId,
       decimal? CaevServiceFee,
       DateTime? CaevCreatedDate
    );
}

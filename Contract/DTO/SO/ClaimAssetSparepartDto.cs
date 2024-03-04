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
using Contract.DTO.Partners;

namespace Contract.DTO.SO
{
    public record ClaimAssetSparepartDto(
       int CaspId,
       string? CaspItemName,
       int? CaspQuantity,
       decimal? CaspItemPrice,
       decimal? CaspSubtotal,
       int? CaspPartEntityid,
       string? CaspSeroId,
       DateTime? CaspCreatedDate,
       PartnerDTO? CaspPartEntity,
       ServiceOrderDto? CaspSero
    );
}

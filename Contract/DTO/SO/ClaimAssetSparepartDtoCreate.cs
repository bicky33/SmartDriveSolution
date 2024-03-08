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
    public record ClaimAssetSparepartDtoCreate(

        [Required(ErrorMessage = "CaspItemName is required.")]
        string? CaspItemName,

        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid CaspQuantity.")]
        int? CaspQuantity,

        [Range(0.01, double.MaxValue, ErrorMessage = "Please provide a valid CaspItemPrice.")]
        decimal? CaspItemPrice,

        [Range(0.01, double.MaxValue, ErrorMessage = "Please provide a valid CaspSubtotal.")]
        decimal? CaspSubtotal,

        [Range(1, int.MaxValue, ErrorMessage = "Please provide a valid CaspPartEntityid.")]
        int? CaspPartEntityid,

        string? CaspSeroId,

        DateTime? CaspCreatedDate
     );
        
}

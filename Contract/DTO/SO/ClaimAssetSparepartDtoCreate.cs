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
    public class ClaimAssetSparepartDtoCreate
    {
        public int? CaspId { get; init; }

        public string? CaspItemName { get; init; }

        public int? CaspQuantity { get; init; }

        public decimal? CaspItemPrice { get; init; }

        public decimal? CaspSubtotal { get; init; }

        public int? CaspPartEntityid { get; init; }

        public string? CaspSeroId { get; init; }

        public DateTime? CaspCreatedDate { get; init; }
    }
}

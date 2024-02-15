using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Response
{
    public class CustomerClaimDto
    {
        public int CuclCreqEntityid { get; set; }
        public int? CuclEvents { get; set; }
        public DateTime? CuclCreateDate { get; set; }
        public decimal? CuclEventPrice { get; set; }
        public decimal? CuclSubtotal { get; set; }
        public string? CuclReason { get; set; }
    }
}

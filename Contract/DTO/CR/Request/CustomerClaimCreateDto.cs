using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Request
{
    public class CustomerClaimCreateDto
    {
        public int CuclCreqEntityid { get; set; }
        public int? CuclEvents { get; set; }
        public DateTime? CuclCreateDate { get; set; }
        public decimal? CuclEventPrice { get; set; }
        public decimal? CuclSubtotal { get; set; }
        public string? CuclReason { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Request
{
    public class CustomerCloseRequestDto
    {
        public int CreqEntityid { get; set; }
        public DateTime? CuclCreateDate { get; set; }
        public string? CuclReason { get; set; }
    }
}

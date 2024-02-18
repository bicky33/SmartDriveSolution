using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Request
{
    public class CustomerRequestUpdateDto
    {
        public int CreqEntityid { get; set; }
        public DateTime? CreqCreateDate { get; set; }
        public string? CreqStatus { get; set; }
        public string? CreqType { get; set; }
        public DateTime? CreqModifiedDate { get; set; }
        public int? CreqCustEntityid { get; set; }
        public int? CreqAgenEntityid { get; set; }
    }
}

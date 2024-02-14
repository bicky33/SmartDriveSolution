using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.SO
{
    public class CreateServicePolisDto
    {
        public int CreqId { get; init; }
        public int CustId { get; init; }
        public int AgentId { get; init; }
        public DateTime CreatePolisDate { get; init; }
        public DateTime PolisStartDate { get; init; }
        public DateTime PolisEndDate { get; init; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Request
{
    public class CustomerInscExtendRequestDto
    {
        public int CuexId { get; set; }
        public int CuexCreqEntityid { get; set; }
        public string? CuexName { get; set; }
        public int? CuexTotalItem { get; set; }
        public decimal? CuexNominal { get; set; }
    }
}

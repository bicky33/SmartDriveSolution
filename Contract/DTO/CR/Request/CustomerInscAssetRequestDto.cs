using Domain.Entities.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Request
{
    public class CustomerInscAssetRequestDto
    {
        public string CiasPoliceNumber { get; set; } = null!;
        public string CiasYear { get; set; } = null!;
        public decimal? CiasCurrentPrice { get; set; }
        public string? CiasPaidType { get; set; }
        public string? CiasIsNewChar { get; set; }
        public int? CiasCarsId { get; set; }
        public string? CiasIntyName { get; set; }
        public int? CiasCityId { get; set; }
        //public virtual ICollection<CustomerInscExtend> CustomerInscExtends { get; set; } = new List<CustomerInscExtend>();
    }
}

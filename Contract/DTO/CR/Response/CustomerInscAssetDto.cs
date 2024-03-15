using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CR;
using Domain.Entities.Master;

namespace Contract.DTO.CR.Response
{
    public class CustomerInscAssetDto
    {
        public int CiasCreqEntityid { get; set; }
        public string CiasPoliceNumber { get; set; } = null!;
        public string CiasYear { get; set; } = null!;
        public DateTime CiasStartdate { get; set; }
        public DateTime CiasEnddate { get; set; }
        public decimal CiasCurrentPrice { get; set; }
        public decimal CiasInsurancePrice { get; set; }
        public decimal CiasTotalPremi { get; set; }
        public string CiasPaidType { get; set; }
        public string CiasIsNewChar { get; set; }
        public int CiasCarsId { get; set; }
        public string CiasIntyName { get; set; }
        public int CiasCityId { get; set; }
        public CarSeries? CiasCars { get; set; }
        public City? CiasCity { get; set; }
        //public CustomerRequest CiasCreqEntity { get; set; } = null!;
        public InsuranceType? CiasIntyNameNavigation { get; set; }
        public ICollection<CustomerInscDoc> CustomerInscDocs { get; set; } = new List<CustomerInscDoc>();
        public ICollection<CustomerInscExtend> CustomerInscExtends { get; set; } = new List<CustomerInscExtend>();
    }
}

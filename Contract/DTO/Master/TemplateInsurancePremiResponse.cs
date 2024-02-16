using Domain.Entities.Master;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.Master
{
    public class TemplateInsurancePremiResponse
    {
        public int TemiId { get; set; }
        public string? TemiName { get; set; }
        public double? TemiRateMin { get; set; }
        public double? TemiRateMax { get; set; }
        public double? TemiNominal { get; set; }
        public string? TemiType { get; set; }
        public int? TemiZonesId { get; set; }
        public string? TemiIntyName { get; set; }
        public int? TemiCateId { get; set; }
    }
}

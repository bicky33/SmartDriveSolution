using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR
{
    public class TemplateSalaryDto
    {
        public int TesalId { get; set; }


        public string? TesalName { get; set; }


        public decimal? TesalNominal { get; set; }

        public double? TesalRateMin { get; set; }

        public double? TesalRateMax { get; set; }
    }
}

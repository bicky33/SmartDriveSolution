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
    public class BatchEmployeeSalaryDto
    {
   
        public int BesaEmpEntityId { get; set; }

        public DateTime BesaCreatedDate { get; set; }

        public DateTime? EmsTrasferDate { get; set; }

        public decimal? BesaTotalSalary { get; set; }

        public string? BesaAccountNumber { get; set; }

        public string? BesaStatus { get; set; }

        public string? BesaPatrTrxno { get; set; }

        public DateTime? BesaPaidDate { get; set; }

        public DateTime? BesaModifiedDate { get; set; }
    }
}

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
    public class EmployeeSalaryDetailDto
    {

        public int EmsaId { get; set; }


        public int EmsaEmpEntityid { get; set; }


        public DateTime EmsaCreateDate { get; set; }


        public string? EmsaName { get; set; }

        public decimal? EmsaSubtotal { get; set; }
    }
}

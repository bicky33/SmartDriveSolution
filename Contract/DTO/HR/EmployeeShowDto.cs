using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR
{
    public class EmployeeShowDto
    {
        public int EmpEntityid { get; set; }
        public string? EmpName { get; set; }

        public DateTime? EmpJoinDate { get; set; }

        public string? EmpGraduate { get; set; }

        public decimal? EmpNetSalary { get; set; }

        //public string? EmpStatus { get; set; } 
        public string? EmpAccountNumber { get; set; }


        public JobTypeShowDto? EmpJobCodeNavigation { get; set; }
    }
}

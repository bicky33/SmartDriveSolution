using Contract.DTO.HR.CompositeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.UpdateEmployee
{
    public class EmployeeUpdateDto
    {
        public string? EmpName { get; set; }


        public string? EmpType { get; set; }


        public string? EmpStatus { get; set; }

        public string? EmpGraduate { get; set; }


        public decimal? EmpNetSalary { get; set; }


        public string? EmpAccountNumber { get; set; }


        public DateTime? EmpModifiedDate { get; set; }


        public string? EmpJobCode { get; set; }

        public virtual UserUpdateDto UserComposite { get; set; } = null!;
        public bool grantUser { get; set; }
    }
}

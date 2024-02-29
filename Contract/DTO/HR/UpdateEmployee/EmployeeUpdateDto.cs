using Contract.DTO.HR.CompositeDto;
using Domain.Enum.HR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.UpdateEmployee
{
    public class EmployeeUpdateDto
    {
        [StringLength(85)]
        public string? EmpName { get; set; }




        public string? EmpStatus { get; set; }

        //public string? EmpGraduate { get; set; }
        public EmployeeGraduate? EmpGraduate { get; set; }  


        public decimal? EmpNetSalary { get; set; }

        [StringLength(15)]
        public string? EmpAccountNumber { get; set; }


        public DateTime? EmpModifiedDate { get; set; }

        [StringLength(5)]
        public string? EmpJobCode { get; set; }

        public virtual UserUpdateDto UserComposite { get; set; } = null!;
        public bool grantUser { get; set; }
    }
}

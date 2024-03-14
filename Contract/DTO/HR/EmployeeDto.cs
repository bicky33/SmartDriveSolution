using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.HR;
using Domain.Enum.HR;

namespace Contract.DTO.HR
{
    public class EmployeeDto
    {
        public int Entityid { get; set; }
        [StringLength(85)]
        public string? EmpName { get; set; }


        public DateTime? EmpJoinDate { get; set; }

        public EmployeeType? EmpType { get; set; }


        public Status? EmpStatus { get; set; }

        public EmployeeGraduate? EmpGraduate { get; set; }

        public decimal? EmpNetSalary { get; set; }

        [StringLength(15)]
        public string? EmpAccountNumber { get; set; }


        public DateTime? EmpModifiedDate { get; set; }

        [StringLength(10)]
        public string? EmpJobCode { get; set; }
/*        public virtual ICollection<EmployeeAreaWorkGroupDto> EmployeeAreWorkgroups { get; set; } = new List<EmployeeAreaWorkGroupDto>();

        [InverseProperty("EmsaEmpEntity")]
        public virtual ICollection<EmployeeSalaryDetailDto> EmployeeSalaryDetails { get; set; } = new List<EmployeeSalaryDetailDto>();*/
    }
}

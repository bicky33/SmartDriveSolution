using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.HR;

namespace Contract.DTO.HR
{
    public class EmployeeDto
    {
        public int Entityid { get; set; }
        public string? EmpName { get; set; }


        public DateTime? EmpJoinDate { get; set; }

        public string? EmpType { get; set; }


        public string? EmpStatus { get; set; }

        public string? EmpGraduate { get; set; }

        public decimal? EmpNetSalary { get; set; }


        public string? EmpAccountNumber { get; set; }


        public DateTime? EmpModifiedDate { get; set; }

        public string? EmpJobCode { get; set; }
        //public virtual ICollection<EmployeeAreaWorkGroupDto> EmployeeAreWorkgroups { get; set; } = new List<EmployeeAreaWorkGroupDto>();

        [InverseProperty("EmsaEmpEntity")]
        public virtual ICollection<EmployeeSalaryDetailDto> EmployeeSalaryDetails { get; set; } = new List<EmployeeSalaryDetailDto>();
    }
}

using Domain.Entities.HR;
using Domain.Entities.Users;
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
    public class EmployeeDto
    {
        [Key]
        [Column("emp_entityid")]
        public int EmpEntityid { get; set; }

        [Column("emp_name")]
        [StringLength(85)]
        [Unicode(false)]
        public string? EmpName { get; set; }

        [Column("emp_join_date", TypeName = "datetime")]
        public DateTime? EmpJoinDate { get; set; }

        [Column("emp_type")]
        [StringLength(15)]
        [Unicode(false)]
        public string? EmpType { get; set; }

        [Column("emp_status")]
        [StringLength(15)]
        [Unicode(false)]
        public string? EmpStatus { get; set; }

        [Column("emp_graduate")]
        [StringLength(15)]
        [Unicode(false)]
        public string? EmpGraduate { get; set; }

        [Column("emp_net_salary", TypeName = "money")]
        public decimal? EmpNetSalary { get; set; }

        [Column("emp_account_number")]
        [StringLength(15)]
        [Unicode(false)]
        public string? EmpAccountNumber { get; set; }

        [Column("emp_modified_date", TypeName = "datetime")]
        public DateTime? EmpModifiedDate { get; set; }

        [Column("emp_job_code")]
        [StringLength(15)]
        [Unicode(false)]
        public string? EmpJobCode { get; set; }

        [ForeignKey("EmpEntityid")]
        [InverseProperty("Employee")]
        public virtual User EmpEntity { get; set; } = null!;

        [ForeignKey("EmpJobCode")]
        [InverseProperty("Employees")]
        public virtual JobType? EmpJobCodeNavigation { get; set; }

        [InverseProperty("EawgEntity")]
        public virtual ICollection<EmployeeAreWorkgroup> EmployeeAreWorkgroups { get; set; } = new List<EmployeeAreWorkgroup>();

        [InverseProperty("EmsaEmpEntity")]
        public virtual ICollection<EmployeeSalaryDetail> EmployeeSalaryDetails { get; set; } = new List<EmployeeSalaryDetail>();
    }
}

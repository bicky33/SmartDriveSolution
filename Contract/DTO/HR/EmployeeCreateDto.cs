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
using Contract.DTO.UserModule;
using Contract.DTO.HR.CompositeDto;
using Domain.Enum.HR;

namespace Contract.DTO.HR
{
    public class EmployeeCreateDto
    {

        [StringLength(85)]

        public string? EmpName { get; set; }


        public DateTime? EmpJoinDate { get; set; }



        public EmployeeType? EmpType { get; set; }


        public Status? EmpStatus { get; set; }

        public EmployeeGraduate? EmpGraduate { get; set; }


        public decimal? EmpNetSalary { get; set; }

        [StringLength(15)]
        public string? EmpAccountNumber { get; set; }


      //  public DateTime? EmpModifiedDate { get; set; }

        [StringLength(3)]
        public string? EmpJobCode { get; set; }
       // public JobTypeShowDto? EmpJobCodeNavigation { get; set; }

        public virtual UserCompositeDto EmpEntity { get; set; } = null!;
        public bool grantUser {  get; set; }

       // [Column("soft_delete")]
       // public string? SoftDelete { get; set; }

        // public virtual JobTypeDto? JobTypeDto { get; set; }


    }
}

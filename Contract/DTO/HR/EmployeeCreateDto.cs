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

namespace Contract.DTO.HR
{
    public class EmployeeCreateDto
    {


        public string? EmpName { get; set; }


        public DateTime? EmpJoinDate { get; set; }



        public string? EmpType { get; set; }


        public string? EmpStatus { get; set; }

        public string? EmpGraduate { get; set; }


        public decimal? EmpNetSalary { get; set; }


        public string? EmpAccountNumber { get; set; }


        public DateTime? EmpModifiedDate { get; set; }


        public string? EmpJobCode { get; set; }

        public virtual UserCompositeDto UserComposite { get; set; } = null!;

       // public virtual JobTypeDto? JobTypeDto { get; set; }


    }
}

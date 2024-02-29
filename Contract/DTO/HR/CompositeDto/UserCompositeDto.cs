using Domain.Entities.CR;
using Domain.Entities.HR;
using Domain.Entities.Partners;
using Domain.Entities.Payment;
using Domain.Entities.SO;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.CompositeDto
{
    public class UserCompositeDto
    {


        public string UserEmail { get; set; } = null!;

        public string UserNationalId { get; set; } = null!;
        public string? UserNpwp { get; set; }
        public DateTime? UserModifiedDate { get; set; }


        //public virtual EmployeeDto? EmployeeDto { get; set; }    

        //public virtual BusinessEntityCompositeDto? BusinessEntityCompositeDto { get; set; }
        public virtual UserAddressCompositeDto UserAddressCompositeDto { get; set; } 

        public virtual UserPhoneCompositeDto UserPhoneCompositeDto { get; set; }

        public virtual UserRoleCompositeDto UserRoleCompositeDto { get; set; } 
    }
}

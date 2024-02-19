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

        public int UserEntityid { get; set; }

        public string UserEmail { get; set; } = null!;

        public string UserNationalId { get; set; } = null!;


        public virtual EmployeeDto? EmployeeDto { get; set; }    

        //public virtual BusinessEntityCompositeDto? BusinessEntityCompositeDto { get; set; }
        public virtual ICollection<UserAddressCompositeDto> UserAddressCompositeDto { get; set; } = new List<UserAddressCompositeDto>();

        public virtual ICollection<UserPhoneCompositeDto> UserPhoneCompositeDto { get; set; } = new List<UserPhoneCompositeDto>();

        public virtual ICollection<UserRoleCompositeDto> UserRoleCompositeDto { get; set; } = new List<UserRoleCompositeDto>();
    }
}

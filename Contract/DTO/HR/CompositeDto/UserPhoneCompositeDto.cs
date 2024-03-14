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
    public class UserPhoneCompositeDto
    {

        public string UsphPhoneNumber { get; set; } = null!;


        public string? UsphPhoneType { get; set; }
      //  public DateTime? UsphModifiedDate { get; set; }



    }
}

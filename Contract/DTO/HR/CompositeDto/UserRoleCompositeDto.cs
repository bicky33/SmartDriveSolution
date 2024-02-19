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
    public class UserRoleCompositeDto
    {
        public int UsroEntityid { get; set; }
        public string UsroRoleName { get; set; } = null!;

        public string? UsroStatus { get; set; }


    }
}

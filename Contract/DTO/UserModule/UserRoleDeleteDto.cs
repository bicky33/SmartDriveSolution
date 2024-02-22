using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.UserModule
{
    public class UserRoleDeleteDto
    {
        [Required]
        public string UsroRoleName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.UserModule
{
    public class RoleDto
    {
        [StringLength(55)]
        public string RoleName { get; set; }

        [StringLength(55)]
        public string RoleDescription { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.UserModule
{
    public class UserRoleDto
    {
        public int UsroEntityid { get; set; }

        [StringLength(2)]
        [Required]
        public string UsroRoleName { get; set; }

        [StringLength(15)]
        [Required]
        public string UsroStatus { get; set; }

        public DateTime? UsroModifiedDate { get; set; }
    }
}

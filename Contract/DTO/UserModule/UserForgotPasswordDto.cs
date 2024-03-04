using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.UserModule
{
    public class UserForgotPasswordDto
    {

        [StringLength(15)]
        public string UserName { get; set; }

        [StringLength(55)]
        public string UserBirthPlace { get; set; }

        [StringLength(20)]
        public string UserNationalId { get; set; }

        [StringLength(35)]
        public string? UserNpwp { get; set; }
        
        [StringLength(100)]
        public string? NewPassword { get; set; }

        [StringLength(100)]
        public string? ConfirmNewPassword { get; set; }
    }
}

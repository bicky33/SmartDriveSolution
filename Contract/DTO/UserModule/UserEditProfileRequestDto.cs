using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.UserModule
{
    public class UserEditProfileRequestDto
    {
        public int? UserEntityid { get; set; }

        [StringLength(15)]
        public string? UserName { get; set; }

        [StringLength(85)]
        public string? UserFullName { get; set; }

        [StringLength(55)]
        public string? UserBirthPlace { get; set; }

        public DateTime? UserBirthDate { get; set; }

        public IFormFile? UserPhoto { get; set; }
    }
}

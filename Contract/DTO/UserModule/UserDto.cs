using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.UserModule
{
    public class UserDto
    {
        public int UserEntityid { get; set; }

        [StringLength(15)]
        public string UserName { get; set; }

        [StringLength(256)]
        public string? UserPassword { get; set; }

        [StringLength(85)]
        public string UserFullName { get; set; }

        [StringLength(25)]
        public string UserEmail { get; set; }

        [StringLength(55)]
        public string UserBirthPlace { get; set; }

        public DateTime UserBirthDate { get; set; }

        [StringLength(20)]
        public string UserNationalId { get; set; }

        [StringLength(35)]
        public string? UserNpwp { get; set; }

        [StringLength(256)]
        public string? UserPhoto { get; set; }

        public DateTime? UserModifiedDate { get; set; }

        public ICollection<UserRoleDto>? UserRoles { get; set; }
        public ICollection<UserPhoneDto>? UserPhones { get; set; }
        public ICollection<UserAddressDto>? UserAddresses { get; set; }
    }
}

using Contract.DTO.HR.CompositeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.UpdateEmployee
{
    public class UserUpdateDto
    {
        public string UserEmail { get; set; } = null!;

        public string UserNationalId { get; set; } = null!;
        public string? UserNpwp { get; set; }

       // public DateTime? UserModifiedDate { get; set; }

        public virtual UserAddressCompositeDto UserAddressCompositeDto { get; set; }

       // public virtual UserPhoneCompositeDto UserPhoneCompositeDto { get; set; }

       // public virtual UserRoleCompositeDto UserRoleCompositeDto { get; set; }
    }
}

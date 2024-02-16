using Domain.Entities.Partners;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.DTO.UserModule;

namespace Contract.DTO.Partners
{
    public record PartnerContactDTO(
        int PacoPatrnEntityid,
        int PacoUserEntityid,
        string? PacoStatus,
        PartnerDTO PacoPatrnEntity,
        UserDto PacoUserEntity
        //ICollection<PartnerAreaWorkgroup> PartnerAreaWorkgroups
    );
}

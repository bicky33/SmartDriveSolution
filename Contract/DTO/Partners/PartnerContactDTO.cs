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
using Domain.Enum;

namespace Contract.DTO.Partners
{
    public record PartnerContactDTO(
        int PacoPatrnEntityid,
        int PacoUserEntityid,
        PartnerStatus PacoStatus,
        string? FullName,
        string? PhoneNumber,
        bool IsGranted
    );
}

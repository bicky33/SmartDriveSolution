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
        [Required(ErrorMessage = $"{nameof(FullName)} is required")]
        string FullName,
        [Required(ErrorMessage = $"{nameof(PhoneNumber)} is required")]
        string PhoneNumber,
        [Required(ErrorMessage = $"{nameof(IsGranted)} is required")]
        bool IsGranted
    );
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enum;

namespace Contract.DTO.Partners
{
    public record PartnerDTO(
        int PartEntityid,
        string? PartName,
        string? PartAddress,
        DateTime? PartJoinDate,
        string? PartAccountNo,
        string? PartNpwp,
        int PartCityId,
        PartnerStatus PartStatus = PartnerStatus.ACTIVE
    );
}

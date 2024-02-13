using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.Partners
{
    public record PartnerRequest(
        int PartEntityid,
        string? PartName,
        string? PartAddress,
        DateTime? PartJoinDate,
        string? PartAccountNo,
        string? PartNpwp,
        string? PartStatus,
        int PartCityId
    );
}

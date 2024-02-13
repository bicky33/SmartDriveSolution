using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.Partners
{
    public record PartnerAreaWorkgroupRequest(
        int PawoPatrEntityid,
        string PawoArwgCode,
        int PawoUserEntityid,
        string? PawoStatus
    );
}

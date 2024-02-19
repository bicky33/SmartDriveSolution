using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Master;
using Domain.Entities.Partners;
using Microsoft.EntityFrameworkCore;
using Domain.Enum;

namespace Contract.DTO.Partners
{
    public record PartnerAreaWorkgroupDTO(
        int PawoPatrEntityid,
        string PawoArwgCode,
        int PawoUserEntityid,
        PartnerStatus PawoStatus, 
        DateTime? PawoModifiedDate
    );
}

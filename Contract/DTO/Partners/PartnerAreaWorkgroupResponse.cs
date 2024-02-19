using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.Partners
{
    public record PartnerAreaWorkgroupResponse(
            int PawoPatrEntityid,
            string PawoArwgCode,
            int PawoUserEntityid,
            PartnerStatus PawoStatus,
            DateTime? PawoModifiedDate,
            string PartName,
            string ArwgDesc,
            string CityName,
            string ProvName,
            string ZonesName
       );
}

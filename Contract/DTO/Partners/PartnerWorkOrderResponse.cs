using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.Partners
{
    public record PartnerWorkOrderResponse(
        string CustomerName,
        string PoliceNumber,
        string ServInsuranceNo,
        DateTime StartDate,
        DateTime EndDate,
        string ServiceType,
        string WorkOrder,
        string Status,
        int SeroPartId,
        string SeroId
    );
}

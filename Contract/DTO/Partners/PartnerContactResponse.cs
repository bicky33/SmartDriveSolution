using Contract.DTO.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.Partners
{
    public record PartnerContactResponse(
        int PacoPatrnEntityid,
        int PacoUserEntityid,
        string? PacoStatus,
        string FullName,
        string PhoneNumber
     );
}

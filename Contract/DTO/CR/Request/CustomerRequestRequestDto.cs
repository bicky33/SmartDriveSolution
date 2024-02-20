using Contract.DTO.CR.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Request
{
    public class CustomerRequestRequestDto
    {
        public int CreqCustEntityid { get; set; }
        public int CreqAgenEntityid { get; set; }
        public CustomerInscAssetRequestDto? CustomerInscAsset { get; set; }
    }
}

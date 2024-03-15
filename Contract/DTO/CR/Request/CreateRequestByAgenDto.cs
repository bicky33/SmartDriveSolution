using Contract.DTO.UserModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Request
{
    public class CreateRequestByAgenDto
    {
        public int? EmpEntityid { get; set; }
        public bool IsGranted { get; set; }
        public string? AreaCode { get; set; }
        public DateTime? CreqCreateDate { get; set; }
        public CustomerCreateDto? CustomerDto { get; set; }
        public CustomerInscAssetRequestDto? CustomerInscAsset { get; set; }
    }
}

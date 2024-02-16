using Contract.DTO.UserModule;
using Domain.Entities.CR;
using Domain.Entities.HR;
using Domain.Entities.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Response
{
    public class CustomerRequestGetDto
    {
        public int CreqEntityid { get; set; }
        public DateTime? CreqCreateDate { get; set; }
        public string? CreqStatus { get; set; }
        public string? CreqType { get; set; }
        //public DateTime? CreqModifiedDate { get; set; }
        //public int? CreqCustEntityid { get; set; }
        //public int? CreqAgenEntityid { get; set; }
        //public EmployeeAreWorkgroup? CreqAgenEntity { get; set; }
        public UserDto? CreqCustEntity { get; set; }
        //public CustomerClaim? CustomerClaim { get; set; }
        public CustomerInscAssetGetDto? CustomerInscAsset { get; set; }
    }
}

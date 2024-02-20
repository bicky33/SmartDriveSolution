using Contract.DTO.UserModule;
using Domain.Entities.HR;
using Domain.Entities.SO;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Response
{
    public class CustomerRequestResponseDto
    {
        public int CreqEntityid { get; set; }
        public DateTime CreqCreateDate { get; set; }
        public EnumCustomerRequest.CREQSTATUS CreqStatus { get; set; }
        public EnumCustomerRequest.CREQTYPE CreqType { get; set; }
        public DateTime CreqModifiedDate { get; set; }
        public int CreqCustEntityid { get; set; }
        public int CreqAgenEntityid { get; set; }
        //public EmployeeAreWorkgroup? CreqAgenEntity { get; set; }
        public UserDto? CreqCustEntity { get; set; }
        public CustomerClaimResponseDto? CustomerClaim { get; set; }
        public CustomerInscAssetResponseDto? CustomerInscAsset { get; set; }
        //public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.HR;
using Domain.Entities.Users;
using Domain.Entities.CR;
using Domain.Entities.SO;
using Domain.Entities.Master;
using Contract.DTO.UserModule;
using Contract.DTO.HR;

namespace Contract.DTO.CR.Response
{
    public class CustomerRequestDto
    {
        public int CreqEntityid { get; set; }
        public DateTime? CreqCreateDate { get; set; }
        public string? CreqStatus { get; set; }        
        public string? CreqType { get; set; }
        public DateTime? CreqModifiedDate { get; set; }
        public int? CreqCustEntityid { get; set; }
        public int? CreqAgenEntityid { get; set; }
        public EmployeeAreaWorkGroupDto? CreqAgenEntity { get; set; }
        public UserDto? CreqCustEntity { get; set; }
        public CustomerClaimDto? CustomerClaim { get; set; }
        public CustomerInscAssetDto? CustomerInscAsset { get; set; }
        //public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}

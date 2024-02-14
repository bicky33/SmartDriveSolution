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
        public EmployeeAreWorkgroup? CreqAgenEntity { get; set; }
        public UserDto? CreqCustEntity { get; set; }
        //public BusinessEntity? CreqEntity { get; set; } = null!;
        public CustomerClaim? CustomerClaim { get; set; }
        public CustomerInscAssetDto? CustomerInscAsset { get; set; }
        public ICollection<Service> Services { get; set; } = new List<Service>();
    }

    public class UserDto
    {
        public int UserEntityid { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        public string? UserFullName { get; set; }
        public string UserEmail { get; set; } = null!;
        public string? UserBirthPlace { get; set; }
        public DateTime? UserBirthDate { get; set; }
        public string UserNationalId { get; set; } = null!;
        public string? UserNpwp { get; set; }
    }

}

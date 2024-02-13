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

namespace Contract.DTO.CR.Request
{
    public class CustomerRequestDto
    {
        public int CreqEntityid { get; set; }

        [Column("creq_create_date", TypeName = "datetime")]
        public DateTime? CreqCreateDate { get; set; }

        [Column("creq_status")]
        [StringLength(15)]
        [Unicode(false)]
        public string? CreqStatus { get; set; }

        [Column("creq_type")]
        [StringLength(15)]
        [Unicode(false)]
        public string? CreqType { get; set; }

        [Column("creq_modified_date", TypeName = "datetime")]
        public DateTime? CreqModifiedDate { get; set; }

        [Column("creq_cust_entityid")]
        public int? CreqCustEntityid { get; set; }

        [Column("creq_agen_entityid")]
        public int? CreqAgenEntityid { get; set; }

        public virtual EmployeeAreWorkgroup? CreqAgenEntity { get; set; }

        [InverseProperty("CustomerRequests")]
        public virtual User? CreqCustEntity { get; set; }

        [InverseProperty("CustomerRequest")]
        public virtual BusinessEntity CreqEntity { get; set; } = null!;

        [InverseProperty("CuclCreqEntity")]
        public virtual CustomerClaim? CustomerClaim { get; set; }

        [InverseProperty("CiasCreqEntity")]
        public virtual CustomerInscAsset? CustomerInscAsset { get; set; }

        [InverseProperty("ServCreqEntity")]
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}

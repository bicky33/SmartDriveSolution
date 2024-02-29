using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Partners;
using Domain.Entities.HR;
using Contract.DTO.Partners;

namespace Contract.DTO.SO
{
    public class ServiceOrderDto
    {
        [Required]
        [StringLength(25)]
        public string SeroId { get; init; } = null!;

        [StringLength(15)]
        public string? SeroOrdtType { get; init; }

        [StringLength(15)]
        public string? SeroStatus { get; set; }

        [StringLength(256)]
        public string? SeroReason { get; set; }

        [StringLength(12)]
        public string? ServClaimNo { get; init; }

        public DateTime? ServClaimStartdate { get; init; }

        public DateTime? ServClaimEnddate { get; init; }

        public int? SeroServId { get; init; }

        [StringLength(25)]
        public string? SeroSeroId { get; init; }

        public int? SeroAgentEntityid { get; init; }

        public int? SeroPartId { get; init; }

        public  ICollection<BatchPartnerInvoice> BatchPartnerInvoices { get; set; } = new List<BatchPartnerInvoice>();

        public ICollection<ClaimAssetEvidenceDto> Caevs { get; set; } = new List<ClaimAssetEvidenceDto>();

        public ICollection<ClaimAssetSparepartDto> Casps { get; set; } = new List<ClaimAssetSparepartDto>();

        public ICollection<ServiceOrderDto> InverseSeroSero { get; set; } = new List<ServiceOrderDto>();

        //public EmployeeAreWorkgroup? SeroAgentEntity { get; set; }

        public PartnerDTO? SeroPart { get; set; }

        public  ServiceOrderDto? SeroSero { get; set; }

        public  ServiceDto? SeroServ { get; set; }

        public  List<ServiceOrderTaskDto> Seots { get; set; } = new List<ServiceOrderTaskDto>();
    }
}

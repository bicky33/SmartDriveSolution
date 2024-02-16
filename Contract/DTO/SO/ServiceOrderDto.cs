using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Partners;
using Domain.Entities.HR;

namespace Contract.DTO.SO
{
    public class ServiceOrderDto
    {
        [Required]
        public string SeroId { get; init; } = null!;

        public string? SeroOrdtType { get; init; }

        public string? SeroStatus { get; init; }

        public string? SeroReason { get; init; }

        public string? ServClaimNo { get; init; }

        public DateTime? ServClaimStartdate { get; init; }

        public DateTime? ServClaimEnddate { get; init; }

        public int? SeroServId { get; init; }

        public string? SeroSeroId { get; init; }

        public int? SeroAgentEntityid { get; init; }

        public int? SeroPartId { get; init; }

        //public  ICollection<BatchPartnerInvoice> BatchPartnerInvoices { get; set; } = new List<BatchPartnerInvoice>();

        //public ICollection<ClaimAssetEvidenceDto> ClaimAssetEvidences { get; set; } = new List<ClaimAssetEvidenceDto>();

        //public ICollection<ClaimAssetSparepartDto> ClaimAssetSpareparts { get; set; } = new List<ClaimAssetSparepartDto>();

        public ICollection<ServiceOrderDto> InverseSeroSero { get; set; } = new List<ServiceOrderDto>();

        //public EmployeeAreWorkgroup? SeroAgentEntity { get; set; }

        //public Partner? SeroPart { get; set; }

        public  ServiceOrderDto? SeroSero { get; set; }

        public  ServiceDto? SeroServ { get; set; }

        public  ICollection<ServiceOrderTaskDto> ServiceOrderTasks { get; set; } = new List<ServiceOrderTaskDto>();
    }
}

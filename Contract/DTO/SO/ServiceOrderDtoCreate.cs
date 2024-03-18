using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.SO
{
    public class ServiceOrderDtoCreate
    {
        [StringLength(25)]
        public string? SeroId { get; init; } = null!;
        [StringLength(15)]
        public string? SeroOrdtType { get; init; }
        [StringLength(15)]
        public string? SeroStatus { get; init; }
        [StringLength(256)]
        public string? SeroReason { get; init; }
        [StringLength(12)]
        public string? ServClaimNo { get; init; }

        public DateTime? ServClaimStartdate { get; init; }

        public DateTime? ServClaimEnddate { get; init; }

        public int? SeroServId { get; init; }
        [StringLength(25)]
        public string? SeroSeroId { get; init; }

        public int? SeroAgentEntityid { get; init; }

        public int? SeroPartId { get; init; }
    }
}

using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.SO
{
    public class ServiceOrderDtoCreate
    {
        public string? SeroId { get; init; } = null!;

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
    }
}

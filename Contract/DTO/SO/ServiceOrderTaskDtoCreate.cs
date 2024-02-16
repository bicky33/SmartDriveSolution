using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.SO
{
    public class ServiceOrderTaskDtoCreate
    {
        public int? SeotId { get; init; }

        public string? SeotName { get; init; }

        public DateTime? SeotStartdate { get; init; }

        public DateTime? SeotEnddate { get; init; }

        public DateTime? SeotActualStartdate { get; init; }

        public DateTime? SeotActualEnddate { get; init; }

        public string? SeotStatus { get; init; }

        public string? SeotArwgCode { get; init; }

        public string? SeotSeroId { get; init; }
    }
}

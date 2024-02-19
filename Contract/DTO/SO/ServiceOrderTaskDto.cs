using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Master;

namespace Contract.DTO.SO
{
    public class ServiceOrderTaskDto
    {
        [Required]
        public int SeotId { get; init; }

        public string? SeotName { get; init; }

        public DateTime? SeotStartdate { get; init; }

        public DateTime? SeotEnddate { get; init; }

        public DateTime? SeotActualStartdate { get; init; }

        public DateTime? SeotActualEnddate { get; init; }

        public string? SeotStatus { get; init; }

        public string? SeotArwgCode { get; init; }

        public string? SeotSeroId { get; init; }

        public AreaWorkgroup? SeotArwgCodeNavigation { get; set; }

        public ServiceOrderDto? SeotSero { get; set; }

        public ICollection<ServiceOrderWorkorderDto> ServiceOrderWorkorders { get; set; } = new List<ServiceOrderWorkorderDto>();
    }
}

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

        [StringLength(256)]
        public string? SeotName { get; set; }

        public DateTime? SeotStartdate { get; init; }

        public DateTime? SeotEnddate { get; init; }

        public DateTime? SeotActualStartdate { get; init; }

        public DateTime? SeotActualEnddate { get; init; }

        [StringLength(15)]
        public string? SeotStatus { get; init; }

        [StringLength(15)]
        public string? SeotArwgCode { get; init; }

        [StringLength(25)]
        public string? SeotSeroId { get; init; }

        //public AreaWorkgroup? SeotArwgCodeNavigation { get; set; }

        public ServiceOrderDto? SeotSero { get; set; }

        public ICollection<ServiceOrderWorkorderDto> Sowos { get; set; } = new List<ServiceOrderWorkorderDto>();
    }
}

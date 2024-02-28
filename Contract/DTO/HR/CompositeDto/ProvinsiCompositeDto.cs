using Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.CompositeDto
{
    public class ProvinsiCompositeDto
    {
        public string? ProvName { get; set; }
        public virtual ZoneCompositeDto? ProvZones { get; set; }
    }
}

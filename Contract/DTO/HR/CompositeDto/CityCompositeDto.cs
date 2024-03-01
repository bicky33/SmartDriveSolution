using Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.CompositeDto
{
    public class CityCompositeDto
    {
        public string? CityName { get; set; }
        public virtual ProvinsiCompositeDto? CityProv { get; set; }
    }
}

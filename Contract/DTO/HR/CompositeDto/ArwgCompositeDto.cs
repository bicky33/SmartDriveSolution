using Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.CompositeDto
{
    public class ArwgCompositeDto
    {
        public string ArwgCode { get; set; } = null!;
        public virtual CityCompositeDto? ArwgCity { get; set; }
    }
}

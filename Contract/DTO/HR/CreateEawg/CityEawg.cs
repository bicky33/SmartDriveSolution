using Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.CreateEawg
{
    public class CityEawg
    {
        public int CityId { get; set; }
        public virtual ICollection<ArwgEmployee> AreaWorkgroups { get; set; } = new List<ArwgEmployee>();


    }
}

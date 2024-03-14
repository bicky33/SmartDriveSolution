using Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.CreateEawg
{
    public class ArwgEmployee
    {
        public int? ArwgCityId { get; set; }
        public virtual ICollection<CreateEawgDto> EmployeeAreWorkgroups { get; set; } = new List<CreateEawgDto>();
    }
}

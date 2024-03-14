using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.CreateEawg
{
    public class ArwgEmployeeUpdateDto
    {
        public int? ArwgCityId { get; set; }
        public virtual ICollection<CreateEawgUpdateDto> EmployeeAreWorkgroups { get; set; } = new List<CreateEawgUpdateDto>();
    }
}

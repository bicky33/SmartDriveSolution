using Contract.DTO.HR.CompositeDto;
using Domain.Entities.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR
{
    public class EmployeeAreaWorkGroupShowDto
    {
        public int EawgId { get; set; }

        public string? EawgArwgCode { get; set; }

        public virtual EArwgEmployeeCompositeDto? EawgEntity { get; set; } = null!;
        public ArwgCompositeDto? EawgArwgCodeNavigation { get; set; }
    }
}

using Domain.Entities.CR;
using Domain.Entities.HR;
using Domain.Entities.Master;
using Domain.Entities.SO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.DTO.HR.CompositeDto;

namespace Contract.DTO.HR
{
    public class EmployeeAreaWorkGroupDto
    {

        public int EawgId { get; set; }


        public int EawgEntityid { get; set; }


        public string? EawgStatus { get; set; }


        public string? EawgArwgCode { get; set; }

        public DateTime? EawgModifiedDate { get; set; }
        public ArwgCompositeDto? EawgArwgCodeNavigation { get; set; }

        public string? SoftDelete {get; set; }

        /*        public AreaWorkgroup? EawgArwgCodeNavigation { get; set; }
                public Employee EawgEntity { get; set; } = null!;*/


    }
}

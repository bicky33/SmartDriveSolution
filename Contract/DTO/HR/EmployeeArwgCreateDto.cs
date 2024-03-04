using Domain.Entities.HR;
using Domain.Entities.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR
{
    public class EmployeeArwgCreateDto
    {



        public int EawgEntityid { get; set; }


        public string? EawgStatus { get; set; }


        public string? EawgArwgCode { get; set; }

        public DateTime? EawgModifiedDate { get; set; }

       /* [Column("soft_delete")]
        public string? SoftDelete { get; set; }*/

        //public virtual AreaWorkgroup? EawgArwgCodeNavigation { get; set; }


        //public virtual EmployeeDto EmployeeDto { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.CompositeDto
{
    public class BusinessEntityCompositeDto
    {

        public int Entityid { get; set; }
        public DateTime EntityModifiedDate { get; set; }
        public virtual UserCompositeDto? UserComposite { get; set; }
    }
}

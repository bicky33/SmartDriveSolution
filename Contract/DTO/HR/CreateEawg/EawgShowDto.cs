using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.CreateEawg
{
    public class EawgShowDto
    {
        public int EawgId { get; set; }

        public string? EawgArwgCode { get; set; }
        public ArwgShowDto EawgArwgCodeNavigation { get; set;}
    }
}

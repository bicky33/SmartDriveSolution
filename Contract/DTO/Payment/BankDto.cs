using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.Payment
{
    public class BankDto
    {
        public int BankEntityid { get; set; }
        public string? BankName { get; set; }
        public string? BankDesc { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Contract.DTO.Payment
{
    public class BankDto
    {
        [HiddenInput]
        public int BankEntityid { get; set; }
        public string? BankName { get; set; }
        public string? BankDesc { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Contract.DTO.Payment
{
    public class BankDto
    {
        [JsonIgnore]
        public int BankEntityid { get; set; }
        [StringLength(5)]
        public string? BankName { get; set; }
        [StringLength(55)]

        public string? BankDesc { get; set; }
    }

}

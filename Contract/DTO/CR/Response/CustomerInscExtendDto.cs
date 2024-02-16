using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Response
{
    public class CustomerInscExtendDto
    {
        public int CuexId { get; set; }
        public int CuexCreqEntityid { get; set; }
        public string? CuexName { get; set; }
        public int? CuexTotalItem { get; set; }
        public decimal? CuexNominal { get; set; }
    }
}

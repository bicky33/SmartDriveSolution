using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class CarBrandResponse
    {
        public int CabrId { get; set; }

        [StringLength(55)]
        [Unicode(false)]
        public string? CabrName { get; set; }
    }
}
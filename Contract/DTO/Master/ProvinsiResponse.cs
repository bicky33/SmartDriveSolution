using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class ProvinsiResponse

    {
        public int ProvId { get; set; }

        [StringLength(85)]
        [Unicode(false)]
        public string? ProvName { get; set; }

        public int? ProvZonesId { get; set; }
    }
}
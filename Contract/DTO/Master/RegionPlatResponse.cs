using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class RegionPlatResponse

    {
        [StringLength(3)]
        [Unicode(false)]
        public string RegpName { get; set; } = null!;

        [StringLength(35)]
        [Unicode(false)]
        public string? RegpDesc { get; set; }

        public int? RegpProvId { get; set; }
    }
}
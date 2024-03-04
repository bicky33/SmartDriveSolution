using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class AreaWorkgroupResponse

    {
        [StringLength(15)]
        [Unicode(false)]
        public string ArwgCode { get; set; } = null!;

        [StringLength(55)]
        [Unicode(false)]
        public string? ArwgDesc { get; set; }

        public int? ArwgCityId { get; set; }
    }
}
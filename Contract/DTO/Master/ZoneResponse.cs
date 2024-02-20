using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class ZoneResponse

    {
        public int ZonesId { get; set; }

        [StringLength(25)]
        [Unicode(false)]
        public string? ZonesName { get; set; }
    }
}
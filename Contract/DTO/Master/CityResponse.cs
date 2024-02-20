using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class CityResponse

    {
        public int CityId { get; set; }
        public int? CityProvId { get; set; }

        [StringLength(85)]
        [Unicode(false)]
        public string? CityName { get; set; }
    }
}
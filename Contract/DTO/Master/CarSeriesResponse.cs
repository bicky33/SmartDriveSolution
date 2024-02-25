using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class CarSeriesResponse
    {
        public int CarsId { get; set; }

        [StringLength(55)]
        [Unicode(false)]
        public string? CarsName { get; set; }

        public int? CarsPassenger { get; set; }
        public int? CarsCarmId { get; set; }
    }
}
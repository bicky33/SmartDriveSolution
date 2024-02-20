using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class CarModelResponse
    {
        public int CarmId { get; set; }

        [StringLength(55)]
        [Unicode(false)]
        public string? CarmName { get; set; }
        public int? CarmCabrId { get; set; }
    }
}
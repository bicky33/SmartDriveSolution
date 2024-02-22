using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class InsuranceTypeResponse

    {
        [StringLength(25)]
        [Unicode(false)]
        public string IntyName { get; set; } = null!;

        [StringLength(25)]
        [Unicode(false)]
        public string? IntyDesc { get; set; }
    }
}
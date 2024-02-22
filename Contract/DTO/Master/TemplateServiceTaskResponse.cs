using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class TemplateServiceTaskResponse
    {
        public int TestaId { get; set; }

        [StringLength(55)]
        [Unicode(false)]
        public string? TestaName { get; set; }

        public int? TestaTetyId { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string? TestaGroup { get; set; }
    }
}
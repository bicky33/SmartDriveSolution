using Domain.Enum.Master;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class TemplateTypeResponse
    {
        public int TetyId { get; set; }

        [StringLength(25)]
        [Unicode(false)]
        public TemplateTypeNameEnum TetyName { get; set; }

        [StringLength(15)]
        [Unicode(false)]
        public TemplateTypeGroupEnum TetyGroup { get; set; }
    }
}
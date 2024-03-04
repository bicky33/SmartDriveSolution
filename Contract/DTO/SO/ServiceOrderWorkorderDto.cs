using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.SO
{
    public class ServiceOrderWorkorderDto
    {
        [Required]
        public int SowoId { get; set; }

        [StringLength(256)]
        public string? SowoName { get; set; }

        public DateTime? SowoModifiedDate { get; set; }

        [StringLength(15)]
        public string? SowoStatus { get; set; }

        public int? SowoSeotId { get; set; }

        public virtual ServiceOrderTaskDto? SowoSeot { get; set; }
    }
}

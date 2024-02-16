using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.SO
{
    public class ServiceOrderWorkorderDtoCreate
    {
        public int? SowoId { get; set; }

        public string? SowoName { get; set; }

        public DateTime? SowoModifiedDate { get; set; }

        public string? SowoStatus { get; set; }

        public int? SowoSeotId { get; set; }

    }
}

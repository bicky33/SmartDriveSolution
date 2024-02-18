using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.UserModule
{
    public class UserPhoneDto
    {
        public int UsphEntityid { get; set; }

        [StringLength(15)]
        public string UsphPhoneNumber { get; set; } = null!;

        [StringLength(15)]
        public string UsphPhoneType { get; set; }

        [StringLength(512)]
        public string? UsphMime { get; set; }

        [StringLength(15)]
        public string? UsphStatus { get; set; }

        public DateTime? UsroModifiedDate { get; set; }
    }
}

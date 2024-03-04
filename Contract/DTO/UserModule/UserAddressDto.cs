using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.DTO.Master;

namespace Contract.DTO.UserModule
{
    public class UserAddressDto
    {
        public int UsdrId { get; set; }
        public int? UsdrEntityid { get; set; }

        [StringLength(255)]
        public string? UsdrAddress1 { get; set; }

        [StringLength(255)]
        public string? UsdrAddress2 { get; set; }

        public DateTime? UsdrModifiedDate { get; set; }

        public int UsdrCityId { get; set; }

        public CityResponse? UsdrCity { get; set; }
    }
}

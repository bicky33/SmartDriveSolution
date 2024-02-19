using Domain.Entities.Master;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.HR.CompositeDto
{
    public class UserAddressCompositeDto
    {

        public string? UsdrAddress1 { get; set; }

        public string? UsdrAddress2 { get; set; }

        public int? UsdrCityId { get; set; }


    }
}

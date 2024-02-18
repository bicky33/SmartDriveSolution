using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.CR;
using Domain.Entities.Users;
using Domain.Entities.SO;

namespace Contract.DTO.SO
{
    public class ServiceDto
    {
        [Required]
        public int ServId { get; init; }

        public DateTime? ServCreatedOn { get; init; }

        public string? ServType { get; set; }

        public string? ServInsuranceNo { get; init; }

        public string? ServVehicleNo { get; init; }

        public DateTime? ServStartdate { get; init; }

        public DateTime? ServEnddate { get; init; }

        public string? ServStatus { get; set; }

        public int? ServServId { get; init; }

        public int? ServCustEntityid { get; init; }

        public int? ServCreqEntityid { get; init; }

        public ICollection<ServiceDto> InverseServServ { get; set; } = new List<ServiceDto>();

        //public CustomerRequest? ServCreqEntity { get; set; }

        //public User? ServCustEntity { get; set; }

        public ServiceDto? ServServ { get; set; }

        public ICollection<ServiceOrderDto> ServiceOrders { get; set; } = new List<ServiceOrderDto>();

        public ServicePremiDto? ServicePremi { get; set; }

        public ICollection<ServicePremiCreditDto> ServicePremiCredits { get; set; } = new List<ServicePremiCreditDto>();
    }
}

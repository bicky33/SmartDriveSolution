using Contract.DTO.CR.Response;
using Contract.DTO.UserModule;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.SO
{
    public class ServiceDto
    {
        [Required]
        public int ServId { get; init; }

        public DateTime? ServCreatedOn { get; init; }

        [StringLength(15)]
        public string? ServType { get; set; }

        [StringLength(12)]
        public string? ServInsuranceNo { get; init; }

        [StringLength(12)]
        public string? ServVehicleNo { get; init; }

        public DateTime? ServStartdate { get; init; }

        public DateTime? ServEnddate { get; init; }

        [StringLength(15)]
        public string? ServStatus { get; set; }

        public int? ServServId { get; init; }
        [Required]
        public int? ServCustEntityid { get; init; }
        [Required]
        public int? ServCreqEntityid { get; init; }

        public ICollection<ServiceDto> InverseServServ { get; set; } = new List<ServiceDto>();

        public CustomerRequestDto? ServCreqEntity { get; set; }

        public UserDto? ServCustEntity { get; set; }

        public ServiceDto? ServServ { get; set; }

        public List<ServiceOrderDto> Seros { get; set; } = new List<ServiceOrderDto>();

        public ServicePremiDto? ServicePremi { get; set; }

        public ICollection<ServicePremiCreditDto> Secrs { get; set; } = new List<ServicePremiCreditDto>();
    }
}

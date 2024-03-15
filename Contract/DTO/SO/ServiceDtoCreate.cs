using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.SO
{
    public class ServiceDtoCreate
    {
        public int? ServId { get; init; }

        public DateTime? ServCreatedOn { get; init; }
        [StringLength(15)]
        public string? ServType { get; init; }
        [StringLength(12)]
        public string? ServInsuranceNo { get; init; }
        [StringLength(12)]
        public string? ServVehicleNo { get; init; }

        public DateTime? ServStartdate { get; init; }

        public DateTime? ServEnddate { get; init; }
        [StringLength(15)]
        public string? ServStatus { get; init; }

        public int? ServServId { get; init; }

        public int? ServCustEntityid { get; init; }

        public int? ServCreqEntityid { get; init; }
    }
}

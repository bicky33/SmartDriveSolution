using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.SO
{
    public class ServiceDtoCreate
    {
        public int? ServId { get; init; }

        public DateTime? ServCreatedOn { get; init; }

        public string? ServType { get; init; }

        public string? ServInsuranceNo { get; init; }

        public string? ServVehicleNo { get; init; }

        public DateTime? ServStartdate { get; init; }

        public DateTime? ServEnddate { get; init; }

        public string? ServStatus { get; init; }

        public int? ServServId { get; init; }

        public int? ServCustEntityid { get; init; }

        public int? ServCreqEntityid { get; init; }
    }
}

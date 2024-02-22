using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.DTO.SO;

namespace Contract.DTO.SO.Composite
{
    public class ServiceCompositeDto
    {
        public DateTime? ServCreatedOn { get; init; }
        public string? ServType { get; init; }
        public string? ServInsuranceNo { get; init; }
        public string? ServVehicleNo { get; init; }
        public string? SeroId { get; init; }
        public string? SeroStatus { get; init; }
        public string? UserFullName { get; init; }
    }
}

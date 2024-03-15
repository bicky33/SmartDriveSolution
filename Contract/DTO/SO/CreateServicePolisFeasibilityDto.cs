using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.SO
{
    public class CreateServicePolisFeasibilityDto
    {
        //[Required]
        public int? CreqId { get; init; }
        //[Required]
        public int? CustId { get; init; }
        //[Required]
        public int? AgentId { get; init; }
        [Required]
        public string ServVehicleNo { get; init; }
        [Required]
        public DateTime CreatePolisDate { get; init; }
        [Required]
        public DateTime PolisStartDate { get; init; }
        [Required]
        public DateTime PolisEndDate { get; init; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.SO
{
    public class CreateClaimPolisDto
    {
        [Required]
        public int ServId { get; init; }
        [Required]
        public int AgentId { get; init; }
        [Required]
        public DateTime CreateClaimDate { get; init; }
        [Required]
        public DateTime ClaimStartDate { get; init; }
        [Required]
        public DateTime ClaimEndDate { get; init; }
    }
}

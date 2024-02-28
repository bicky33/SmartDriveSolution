using System.ComponentModel.DataAnnotations;

namespace Domain.RequestFeatured
{
    public class EntityPaymentTransactionParameter : EntityParameter
    {
        [Required]
        public int UserEntityId { get; set; }
        public string? AccountNumber { get; set; }
        public int? AccountBusEntityId { get; set; }
        public bool SortByTime { get; set; }
    }
}

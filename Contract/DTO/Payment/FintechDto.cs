using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contract.DTO.Payment
{
    public class FintechDto
    {
        public int FintEntityid { get; set; }
        [StringLength(5)]
        public string FintName { get; set; }
        [StringLength(55)]
        public string? FintDesc { get; set; }
    }

}

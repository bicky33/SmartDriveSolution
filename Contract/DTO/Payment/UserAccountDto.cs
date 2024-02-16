namespace Contract.DTO.Payment
{
    public class UserAccountDto
    {
        public int UsacId { get; set; }
        public string UsacAccountno { get; set; }
        public decimal? UsacDebet { get; set; }
        public decimal? UsacCredit { get; set; }
        public string? UsacType { get; set; }
        public int? UsacBankEntityid { get; set; }
        public int? UsacFintEntityid { get; set; }
        public int? UsacUserEntityid { get; set; }
    }

}

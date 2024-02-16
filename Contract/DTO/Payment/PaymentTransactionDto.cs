namespace Contract.DTO.Payment
{
    public class PaymentTransactionDto
    {
        public string PatrTrxno { get; set; } = null!;
        public DateTime? PatrCreatedOn { get; set; }
        public decimal? PatrDebet { get; set; }
        public decimal? PatrCredit { get; set; }
        public string? PatrUsacAccountNoFrom { get; set; }
        public string? PatrUsacAccountNoTo { get; set; }
        public string? PatrType { get; set; }
        public string? PatrInvoiceNo { get; set; }
        public string? PatrNotes { get; set; }
        public string? PatrTrxnoRev { get; set; }
    }

}

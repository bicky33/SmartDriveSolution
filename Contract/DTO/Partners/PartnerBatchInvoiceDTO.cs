namespace Contract.DTO.Partners
{
    public record PartnerBatchInvoiceDTO(
        string? BpinInvoiceNo,
        DateTime? BpinCreatedOn,
        decimal? BpinSubtotal,
        decimal? BpinTax,
        string? BpinAccountNo,
        DateTime? BpinPaidDate,
        int? BpinPatrnEntityid,
        string? BpinPatrTrxno,
        string BpinSeroId
    );

    public record PartnerBatchInvoiceResponse(
        string? InvoiceNo,
        DateTime? CreateOn,
        string? PolisNumber,
        string? PoliceNumber,
        decimal Subtotal,
        decimal Tax,
        string? AccountNumber,
        DateTime? PaidDate,
        string? PartnerName

    );
}

using Domain.Entities.Partners;
using Domain.Entities.Payment;
using Domain.Entities.SO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}

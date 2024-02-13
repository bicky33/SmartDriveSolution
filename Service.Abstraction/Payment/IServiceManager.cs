using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Payment
{
    public interface IServiceManager
    {
        IServiceEntityBase<BankDto> BankService { get; }
        IServiceEntityBase<FintechDto> FintechService { get; }
        IServiceEntityBase<UserAccountDto> UserAccountService { get; }
        IServiceEntityBase<PaymentTransactionDto> PaymentTransactionService { get; }

    }
}

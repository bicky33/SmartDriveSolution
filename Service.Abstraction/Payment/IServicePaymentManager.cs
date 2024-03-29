﻿using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Payment
{
    public interface IServicePaymentManager
    {
        IServiceEntityBase<BankDto> BankService { get; }
        IServiceEntityBase<FintechDto> FintechService { get; }
        IServiceEntityUserAccount UserAccountService { get; }
        IServiceEntityPaymentTransaction PaymentTransactionService { get; }

    }
}

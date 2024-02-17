using Domain.Entities.Payment;
using Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Payment
{
    public interface IRepositoryPaymentManager
    {
        IRepositoryEntityBase<Bank> BankRepository { get; }
        IRepositoryEntityBase<Fintech> FintechRepository { get; }
        IRepositoryEntityBase<UserAccount> UserAccountRepository { get; }
        IRepositoryEntityBase<PaymentTransaction> PaymentTransactionRepository { get; }

        IUnitOfWorks UnitOfWorks { get; }
    }
}

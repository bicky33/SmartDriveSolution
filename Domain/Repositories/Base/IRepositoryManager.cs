using Domain.Entities.Payment;
using Domain.Repositories;
using Domain.Repositories.Base;

namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IRepositoryEntityBase<Bank> BankRepository { get; }
        IRepositoryEntityBase<Fintech> FintechRepository { get; }
        IRepositoryEntityBase<UserAccount> UserAccountRepository { get; }
        IRepositoryEntityBase<PaymentTransaction> PaymentTransactionRepository { get; }

        IUnitOfWorks UnitOfWorks { get; }
    }
}

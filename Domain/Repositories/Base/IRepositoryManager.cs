using Domain.Entities.Payment;

namespace Domain.Repositories.Base
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

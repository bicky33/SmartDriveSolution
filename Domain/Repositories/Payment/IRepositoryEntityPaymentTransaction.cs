using Domain.Entities.Payment;

namespace Domain.Repositories.Payment
{
    public interface IRepositoryEntityPaymentTransaction
    {
        Task<IEnumerable<PaymentTransaction>> GetAllEntity(bool trackChanges);

        Task<PaymentTransaction> GetEntityById(int id, bool trackChanges);

        void CreateEntity(PaymentTransaction entity);

        void DeleteEntity(PaymentTransaction entity);
        int GetNexTrxSequence();
    }
}

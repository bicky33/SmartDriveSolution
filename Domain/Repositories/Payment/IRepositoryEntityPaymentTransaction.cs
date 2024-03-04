using Domain.Entities.Partners;
using Domain.Entities.Payment;
using Domain.RequestFeatured;

namespace Domain.Repositories.Payment
{
    public interface IRepositoryEntityPaymentTransaction
    {
        Task<IEnumerable<PaymentTransaction>> GetAllEntity(bool trackChanges);

        Task<PaymentTransaction> GetEntityById(int id, bool trackChanges);

        Task<PagedList<PaymentTransaction>> GetAllPaging(bool trackChanges, EntityPaymentTransactionParameter parameter); 
        void CreateEntity(PaymentTransaction entity);

        void DeleteEntity(PaymentTransaction entity);
        int GetNexTrxSequence();
    }
}

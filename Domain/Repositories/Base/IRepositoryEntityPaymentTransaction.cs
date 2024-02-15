using Contract.DTO.Payment;

namespace Domain.Repositories.Base
{
    public interface IServiceEntityPaymentTransaction
    {
        Task<IEnumerable<PaymentTransactionDto>> GetAllAsync(bool trackChanges);
        Task<PaymentTransactionDto> GetByIdAsync(int id, bool trackChanges);
        Task<PaymentTransactionDto> CreateAsync(PaymentTransactionDto entity);
        Task<PaymentTransactionDto> UpdateAsync(int id, PaymentTransactionDto entity);
        Task DeleteAsync(int id);
    }
}

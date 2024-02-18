
using Contract.DTO.Payment;

namespace Service.Abstraction.Payment
{
    public interface IServiceEntityPaymentTransaction
    {
        Task<IEnumerable<PaymentTransactionDto>> GetAllAsync(bool trackChanges);
        Task<PaymentTransactionDto> CreateAsync(PaymentTransactionCreateDto entity);
        //Task<PaymentTransactionDto> GetByIdAsync(int id, bool trackChanges);
    }
}

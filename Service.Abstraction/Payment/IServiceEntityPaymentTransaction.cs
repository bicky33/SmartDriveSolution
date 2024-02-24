
using Contract.DTO.Partners;
using Contract.DTO.Payment;
using Domain.RequestFeatured;

namespace Service.Abstraction.Payment
{
    public interface IServiceEntityPaymentTransaction
    {
        Task<IEnumerable<PaymentTransactionDto>> GetAllAsync(bool trackChanges);
        Task<PaymentTransactionDto> CreateAsync(PaymentTransactionCreateDto entity);
        Task<PaymentTransactionDto> CreateDepositAsync(PaymentTransactionDepositDto entity);
        Task<IEnumerable<PaymentTransactionDto>> GetAllPagingAsync(EntityPaymentTransactionParameter parameter, bool trackChanges);

    }
}

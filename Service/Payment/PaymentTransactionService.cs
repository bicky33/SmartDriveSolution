using Contract.DTO.Payment;
using Service.Abstraction.Base;

namespace Service.Payment
{
    public class PaymentTransactionService : IServiceEntityBase<PaymentTransactionDto>
    {
        public Task<PaymentTransactionDto> CreateAsync(PaymentTransactionDto entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PaymentTransactionDto>> GetAllAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentTransactionDto> GetByIdAsync(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, PaymentTransactionDto entity)
        {
            throw new NotImplementedException();
        }
    }
}

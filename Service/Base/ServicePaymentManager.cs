using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Domain.Repositories.Payment;
using Service.Abstraction.Base;
using Service.Abstraction.Payment;
using Service.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base
{
    public class ServicePaymentManager : IServicePaymentManager
    {
        private readonly Lazy<IServiceEntityBase<BankDto>> _bankService;
        private readonly Lazy<IServiceEntityBase<FintechDto>> _fintechService;
        private readonly Lazy<IServiceEntityBase<UserAccountDto>> _userAccountDtoService;
        private readonly Lazy<IServiceEntityBase<PaymentTransactionDto>> _paymentTransactionService;

        public ServicePaymentManager(IRepositoryPaymentManager categoryService)
        {
            _bankService = new Lazy<IServiceEntityBase<BankDto>>(() => new BankService(categoryService));
            _fintechService = new Lazy<IServiceEntityBase<FintechDto>>(() => new FintechService(categoryService));
            //_userAccountDtoService = new Lazy<IServiceEntityBase<UserAccountDto>>(() => newUserAccountService(categoryService));
            //_paymentTransactionService = new Lazy<IServiceEntityBase<PaymentTransactionDto>>(() => new PaymentTransactionService(categoryService));
        }
         
        public IServiceEntityBase<BankDto> BankService => _bankService.Value;

        public IServiceEntityBase<FintechDto> FintechService => throw new NotImplementedException();

        public IServiceEntityBase<UserAccountDto> UserAccountService => throw new NotImplementedException();

        public IServiceEntityBase<PaymentTransactionDto> PaymentTransactionService => throw new NotImplementedException();
    }
}

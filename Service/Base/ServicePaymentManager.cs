using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Domain.Repositories.Base;
using Domain.Repositories.Payment;
using Domain.Repositories.UserModule;
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
        private readonly Lazy<IServiceEntityBase<UserAccountDto>> _userAccountService;
        private readonly Lazy<IServiceEntityPaymentTransaction > _paymentTransactionService;

        public ServicePaymentManager(IRepositoryPaymentManager repositoryPaymentManager, IRepositoryManagerUser repositoryManagerUser)
        {
            _bankService = new Lazy<IServiceEntityBase<BankDto>>(() => new BankService(repositoryPaymentManager, repositoryManagerUser));
            _fintechService = new Lazy<IServiceEntityBase<FintechDto>>(() => new FintechService(repositoryPaymentManager, repositoryManagerUser));
            _userAccountService = new Lazy<IServiceEntityBase<UserAccountDto>>(() => new UserAccountService(repositoryPaymentManager, repositoryManagerUser));
            _paymentTransactionService = new Lazy<IServiceEntityPaymentTransaction >(() => new PaymentTransactionService(repositoryPaymentManager));
        }

        public IServiceEntityBase<BankDto> BankService => _bankService.Value;

        public IServiceEntityBase<FintechDto> FintechService => _fintechService.Value;

        public IServiceEntityBase<UserAccountDto> UserAccountService => _userAccountService.Value;

        public IServiceEntityPaymentTransaction PaymentTransactionService => _paymentTransactionService.Value;
    }
}

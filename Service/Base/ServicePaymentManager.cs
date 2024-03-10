using Contract.DTO.Payment;
using Domain.Repositories.Partners;
using Domain.Repositories.Payment;
using Domain.Repositories.UserModule;
using Persistence.Repositories;
using Service.Abstraction.Base;
using Service.Abstraction.Payment;
using Service.Payment;

namespace Service.Base
{
    public class ServicePaymentManager : IServicePaymentManager
    {
        private readonly Lazy<IServiceEntityBase<BankDto>> _bankService;
        private readonly Lazy<IServiceEntityBase<FintechDto>> _fintechService;
        private readonly Lazy<IServiceEntityUserAccount> _userAccountService;
        private readonly Lazy<IServiceEntityPaymentTransaction> _paymentTransactionService;

        public ServicePaymentManager(IRepositoryPaymentManager repositoryPaymentManager, IRepositoryManagerUser repositoryManagerUser, IRepositoryPartnerManager repositoryPartnerManager)
        {
            _bankService = new Lazy<IServiceEntityBase<BankDto>>(() => new BankService(repositoryPaymentManager, repositoryManagerUser));
            _fintechService = new Lazy<IServiceEntityBase<FintechDto>>(() => new FintechService(repositoryPaymentManager, repositoryManagerUser));
            _userAccountService = new Lazy<IServiceEntityUserAccount>(() => new UserAccountService(repositoryPaymentManager, repositoryManagerUser));
            _paymentTransactionService = new Lazy<IServiceEntityPaymentTransaction>(() => new PaymentTransactionService(repositoryPaymentManager, repositoryPartnerManager));
        }

        public IServiceEntityBase<BankDto> BankService => _bankService.Value;

        public IServiceEntityBase<FintechDto> FintechService => _fintechService.Value;

        public IServiceEntityUserAccount UserAccountService => _userAccountService.Value;

        public IServiceEntityPaymentTransaction PaymentTransactionService => _paymentTransactionService.Value;
    }
}

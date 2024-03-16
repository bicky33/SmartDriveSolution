using Domain.Entities.Master;
using Domain.Entities.Payment;
using Domain.Repositories.Base;
using Domain.Repositories.Payment;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Payment
{
    public class RepositoryPaymentManager : IRepositoryPaymentManager
    {
        private readonly Lazy<IUnitOfWorks> _unitOfWorks;
        private readonly Lazy<IRepositoryEntityBase<Bank>> _bankRepository;
        private readonly Lazy<IRepositoryEntityBase<Fintech>> _fintechRepository;
        private readonly Lazy<IRepositoryEntityUserAccount> _userAccountRepository;
        private readonly Lazy<IRepositoryEntityPaymentTransaction> _paymentTransactionRepository;
        public RepositoryPaymentManager(SmartDriveContext dbContext)
        {
            _unitOfWorks = new Lazy<IUnitOfWorks>(() => new UnitOfWorks(dbContext));

            _bankRepository = new Lazy<IRepositoryEntityBase<Bank>>(() => new BankRepository(dbContext));
            _fintechRepository = new Lazy<IRepositoryEntityBase<Fintech>>(() => new FintechRepository(dbContext));
            _userAccountRepository = new Lazy<IRepositoryEntityUserAccount>(() => new UserAccountRepository(dbContext));
            _paymentTransactionRepository = new Lazy<IRepositoryEntityPaymentTransaction>(() => new PaymentTransactionRepository(dbContext));
        }
        public IRepositoryEntityBase<Bank> BankRepository => _bankRepository.Value;

        public IRepositoryEntityBase<Fintech> FintechRepository => _fintechRepository.Value;

        public IRepositoryEntityUserAccount UserAccountRepository => _userAccountRepository.Value;

        public IRepositoryEntityPaymentTransaction PaymentTransactionRepository => _paymentTransactionRepository.Value;

        public IUnitOfWorks UnitOfWorks => _unitOfWorks.Value;
    }
}

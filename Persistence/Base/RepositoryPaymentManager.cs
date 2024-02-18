using Domain.Entities.Master;
using Domain.Entities.Payment;
using Domain.Repositories.Base;
using Domain.Repositories.Payment;
using Persistence.Repositories;
using Persistence.Repositories.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Base
{
    public class RepositoryPaymentManager : IRepositoryPaymentManager
    {
        private readonly Lazy<IUnitOfWorks> _unitOfWorks;
        private readonly Lazy<IRepositoryEntityBase<Bank>> _bankRepository;
        private readonly Lazy<IRepositoryEntityBase<Fintech>> _fintechRepository;
        private readonly Lazy<IRepositoryEntityBase<UserAccount>> _userAccountRepository;
        private readonly Lazy<IRepositoryEntityBase<PaymentTransaction>> _paymentTransactionRepository;
        public RepositoryPaymentManager(SmartDriveContext dbContext)
        {
            _unitOfWorks = new Lazy<IUnitOfWorks>(() => new UnitOfWorks(dbContext));

            _bankRepository = new Lazy<IRepositoryEntityBase<Bank>>(() => new BankRepository(dbContext));
            _fintechRepository = new Lazy<IRepositoryEntityBase<Fintech>>(() => new FintechRepository(dbContext));
            _userAccountRepository = new Lazy<IRepositoryEntityBase<UserAccount>>(() => new UserAccountRepository(dbContext));
            _paymentTransactionRepository = new Lazy<IRepositoryEntityBase<PaymentTransaction>>(() => new PaymentTransactionRepository(dbContext)); 
        }
        public IRepositoryEntityBase<Bank> BankRepository => _bankRepository.Value;

        public IRepositoryEntityBase<Fintech> FintechRepository => _fintechRepository.Value;

        public IRepositoryEntityBase<UserAccount> UserAccountRepository => _userAccountRepository.Value;

        public IRepositoryEntityBase<PaymentTransaction> PaymentTransactionRepository => _paymentTransactionRepository.Value;

        public IUnitOfWorks UnitOfWorks => _unitOfWorks.Value;
    }
}

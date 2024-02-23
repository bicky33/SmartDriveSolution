using Contract.DTO.Payment;
using Domain.Entities.Payment;
using Domain.Enum;
using Domain.Repositories.Payment;
using Mapster;
using Service.Abstraction.Base;
using Service.Abstraction.Payment;
using System.Globalization;

namespace Service.Payment
{
    public class PaymentTransactionService : IServiceEntityPaymentTransaction
    {
        private readonly IRepositoryPaymentManager _repositoryPaymentManager;

        public PaymentTransactionService(IRepositoryPaymentManager repositoryPaymentManager)
        {
            _repositoryPaymentManager = repositoryPaymentManager;
        }

        public async Task<PaymentTransactionDto> CreateAsync(PaymentTransactionCreateDto entity)
        {
            //First Transaction (Sender)
            var sendToTrx = entity.Adapt<PaymentTransaction>();
            var currentDate = DateTime.Now;
            sendToTrx.PatrCreatedOn = currentDate;
            var accountFrom = await _repositoryPaymentManager.UserAccountRepository.GetUserAccountByAccountNo(entity.PatrUsacAccountNoFrom, true);
            var accountTo = await _repositoryPaymentManager.UserAccountRepository.GetUserAccountByAccountNo(entity.PatrUsacAccountNoTo, true);

            //Get Sequence
            var nextseq = _repositoryPaymentManager.PaymentTransactionRepository.GetNexTrxSequence();
            int lastTrxNoToInt = nextseq;

            string formattedCount = lastTrxNoToInt.ToString("0000");
            string formattedDate = $"{currentDate:yyyy-MM-dd}";
            string trxNoResult = $"trx{formattedDate}{formattedCount}";
            string invoiceNoResult = $"SAL-{formattedDate}";

            sendToTrx.PatrDebet = entity.SendAmount;
            //TODO add validation if 
            sendToTrx.PatrTrxno = trxNoResult;
            sendToTrx.PatrInvoiceNo = invoiceNoResult;
            sendToTrx.PatrType = entity.PatrType.ToString();
            sendToTrx.PatrTrxnoRev = null;
            _repositoryPaymentManager.PaymentTransactionRepository.CreateEntity(sendToTrx);
            await _repositoryPaymentManager.UnitOfWorks.SaveChangesAsync();


            //Second Transaction(Receiver) 
            int lastTrxNoToIntNext = _repositoryPaymentManager.PaymentTransactionRepository.GetNexTrxSequence();
            var sendFromTrx = entity.Adapt<PaymentTransaction>();
            string newFormattedCount = lastTrxNoToIntNext.ToString("0000");
            sendFromTrx.PatrCreatedOn = currentDate;
            sendFromTrx.PatrUsacAccountNoTo = sendToTrx.PatrUsacAccountNoTo;
            sendToTrx.PatrUsacAccountNoTo = "-";
            sendFromTrx.PatrUsacAccountNoFrom = "-";
            sendFromTrx.PatrCredit = entity.SendAmount;
            sendFromTrx.PatrTrxnoRev = trxNoResult;
            sendFromTrx.PatrInvoiceNo = sendToTrx.PatrInvoiceNo;
            sendFromTrx.PatrType = entity.PatrType.ToString();
            string trxFromResult = $"trx{formattedDate}{newFormattedCount}";
            sendFromTrx.PatrTrxno = trxFromResult;

            accountFrom.UsacDebet -= entity.SendAmount;
            accountFrom.UsacCredit += entity.SendAmount * -1;

            accountTo.UsacDebet += entity.SendAmount;


            _repositoryPaymentManager.PaymentTransactionRepository.CreateEntity(sendFromTrx);
            await _repositoryPaymentManager.UnitOfWorks.SaveChangesAsync();


            return sendToTrx.Adapt<PaymentTransactionDto>();
        }

        public async Task<PaymentTransactionDto> CreateDepositAsync(PaymentTransactionDepositDto entity)
        {
            var currentDate = DateTime.Now;
            entity.PatrType = PaymentTypeEnum.DEPOSIT;
            int lastTrxNoToIntNext = _repositoryPaymentManager.PaymentTransactionRepository.GetNexTrxSequence();
            var account = await _repositoryPaymentManager.UserAccountRepository.GetUserAccountByAccountNo(entity.PatrUsacAccountNoTo, true);
            string formattedCount = lastTrxNoToIntNext.ToString("0000");
            string formattedDate = $"{currentDate:yyyy-MM-dd}";
            string trxNoResult = $"trx{formattedDate}{formattedCount}";
            string invoiceNoResult = $"SAL-{formattedDate}";

            var sendFromTrx = entity.Adapt<PaymentTransaction>();
            sendFromTrx.PatrCreatedOn = currentDate;
            sendFromTrx.PatrUsacAccountNoTo = null;
            sendFromTrx.PatrUsacAccountNoFrom = "-";
            sendFromTrx.PatrCredit = entity.SendAmount;
            sendFromTrx.PatrTrxnoRev = null;
            sendFromTrx.PatrInvoiceNo = invoiceNoResult;
            sendFromTrx.PatrType = entity.PatrType.ToString();
            sendFromTrx.PatrTrxno = trxNoResult;

            account.UsacDebet += entity.SendAmount;

            _repositoryPaymentManager.PaymentTransactionRepository.CreateEntity(sendFromTrx);
            await _repositoryPaymentManager.UnitOfWorks.SaveChangesAsync();
            return sendFromTrx.Adapt<PaymentTransactionDto>();
        }

        public async Task<IEnumerable<PaymentTransactionDto>> GetAllAsync(bool trackChanges)
        {
            var data = await _repositoryPaymentManager.PaymentTransactionRepository.GetAllEntity(false);
            return data.Adapt<IEnumerable<PaymentTransactionDto>>();
        }

        //public Task<PaymentTransactionDto> GetByIdAsync(int id, bool trackChanges)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

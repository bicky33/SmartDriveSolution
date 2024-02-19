using Contract.DTO.Payment;
using Domain.Entities.Payment;
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
            var sendToTrx = entity.Adapt<PaymentTransaction>();
            var lastTrxNoToInt = 0000;
            var currentDate = DateTime.Now;

            var allData = await _repositoryPaymentManager.PaymentTransactionRepository.GetAllEntity(false);

            if (allData != null)
            {
                var lastTrx = allData.Last();
                var lastTrxNo = lastTrx.PatrTrxno;
                lastTrxNoToInt = Convert.ToInt32(lastTrxNo.Substring(lastTrxNo.Length - 4));
            }
            lastTrxNoToInt++;

            //TODO Adding Credit and debet

            //First Transaction (Sender)
            sendToTrx.PatrTrxnoRev = null;
            string formattedCount = lastTrxNoToInt.ToString("0000");
            sendToTrx.PatrCreatedOn = currentDate;

            string formattedDate = $"{currentDate:yyyy-MM-dd}";
            string trxNoResult = $"trx{formattedDate}{formattedCount}";
            string invoiceNoResult = $"SAL-{formattedDate}";
            sendToTrx.PatrDebet = entity.SendAmount;
            sendToTrx.PatrTrxno = trxNoResult;
            sendToTrx.PatrInvoiceNo = invoiceNoResult;
            _repositoryPaymentManager.PaymentTransactionRepository.CreateEntity(sendToTrx);

            //Second Transaction (Receiver)
            var sendFromTrx = entity.Adapt<PaymentTransaction>();
            lastTrxNoToInt++;
            string newFormattedCount = lastTrxNoToInt.ToString("0000");
            sendFromTrx.PatrCreatedOn = currentDate;
            sendFromTrx.PatrUsacAccountNoTo = sendToTrx.PatrUsacAccountNoTo;
            sendToTrx.PatrUsacAccountNoTo = "-";
            sendFromTrx.PatrUsacAccountNoFrom = "-";
            sendFromTrx.PatrCredit = entity.SendAmount; 
            sendFromTrx.PatrTrxnoRev = trxNoResult;
            sendFromTrx.PatrInvoiceNo = sendToTrx.PatrInvoiceNo;
            string trxFromResult = $"trx{formattedDate}{newFormattedCount}";
            sendFromTrx.PatrTrxno = trxFromResult;

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

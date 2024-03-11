﻿using Contract.DTO.Partners;
using Contract.DTO.Payment;
using Contract.Records;
using Domain.Entities.Partners;
using Domain.Entities.Payment;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Repositories.Partners;
using Domain.Repositories.Payment;
using Domain.RequestFeatured;
using Mapster;
using Persistence.Repositories;
using Service.Abstraction.Base;
using Service.Abstraction.Payment;
using System.Globalization;

namespace Service.Payment
{
    public class PaymentTransactionService : IServiceEntityPaymentTransaction
    {
        private readonly IRepositoryPaymentManager _repositoryPaymentManager;
        private readonly IRepositoryPartnerManager _repositoryPartnerManager;


        public PaymentTransactionService(IRepositoryPaymentManager repositoryPaymentManager, IRepositoryPartnerManager repositoryPartnerManager)
        {
            _repositoryPaymentManager = repositoryPaymentManager;
            _repositoryPartnerManager = repositoryPartnerManager;
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
            sendToTrx.PatrUsacAccountNoTo = entity.PatrUsacAccountNoTo;
            sendFromTrx.PatrUsacAccountNoFrom = entity.PatrUsacAccountNoFrom;
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
            sendFromTrx.PatrUsacAccountNoTo = entity.PatrUsacAccountNoTo;
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

        public async Task GenerateTransferEmployeeAsync()
        {


        }

        public async Task GenerateTransferPartnerAsync()
        {
            var invoices = await _repositoryPartnerManager.RepositoryPartnerBatchInvoice.GetAllData();

            foreach (var item in invoices)
            {
                var invoice = await _repositoryPartnerManager.RepositoryPartnerBatchInvoice.GetByid(item.BpinInvoiceNo);
                var accountNo = await _repositoryPaymentManager.UserAccountRepository.GetUserAccountByAccountNo(item.BpinAccountNo, false)
                    ?? throw new EntityNotFoundException(item.BpinAccountNo, "Account Number");
                PaymentTransactionCreateDto entity = new()
                {
                    SendAmount = item.BpinSubtotal + item.BpinTax,
                    PatrUsacAccountNoFrom = "AXA",
                    PatrUsacAccountNoTo = item.BpinAccountNo,
                    PatrType = PaymentTypeEnum.PAIDPARTNER
                };
                var tr = await CreateAsync(entity);
                invoice.BpinPatrTrxno = tr.PatrTrxno;
                invoice.BpinStatus = BpinStatus.PAID.ToString();
                invoice.BpinPaidDate = DateTime.Now;
                await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();
            }
            await _repositoryPartnerManager.UnitOfWorks.SaveChangesAsync();

        }

        public async Task<IEnumerable<PaymentTransactionDto>> GetAllAsync(bool trackChanges)
        {
            var data = await _repositoryPaymentManager.PaymentTransactionRepository.GetAllEntity(false);
            return data.Adapt<IEnumerable<PaymentTransactionDto>>();
        }

        public async Task<PaginationPaymentDTO<PaymentTransactionDto>> GetAllPagingAsync(EntityPaymentTransactionParameter parameter, bool trackChanges)
        {
            PagedList<PaymentTransaction> partners = await _repositoryPaymentManager.PaymentTransactionRepository.GetAllPaging(trackChanges, parameter);
            IEnumerable<PaymentTransactionDto> partnersDTO = partners.Adapt<IEnumerable<PaymentTransactionDto>>();
            PaginationPaymentDTO<PaymentTransactionDto> pagination = new(partners.TotalCount, partners.CurrentPage, partnersDTO.ToList());
            return pagination;
        }


    }
}

using Domain.Entities.Partners;
using Domain.Entities.Payment;
using Domain.Entities.Users;
using Domain.Repositories.Base;
using Domain.Repositories.Payment;
using Domain.RequestFeatured;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;

namespace Persistence.Repositories.Payment
{
    public class PaymentTransactionRepository : RepositoryBase<PaymentTransaction>, IRepositoryEntityPaymentTransaction
    {
        public PaymentTransactionRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(PaymentTransaction entity)
        {
            Create(entity);
        }

        public void DeleteEntity(PaymentTransaction entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<PaymentTransaction>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.PatrTrxno).ToListAsync();
        }

        public async Task<PagedList<PaymentTransaction>> GetAllPaging(bool trackChanges, EntityPaymentTransactionParameter parameter)
        {
            var transactions = new List<PaymentTransaction>();
            IQueryable<PaymentTransaction> partners = string.IsNullOrEmpty(parameter.SearchBy) ? GetAll(trackChanges) : GetByCondition(c => c.PatrTrxno.StartsWith(parameter.SearchBy), trackChanges);

            var resultOLD = _dbContext.UserAccounts
              .Join(_dbContext.Users,
                  ua => ua.UsacUserEntityid,
                  us => us.UserEntityid,
                  (ua, us) => new { UserAccount = ua, User = us })
              .Join(_dbContext.PaymentTransactions,
                  combined => combined.UserAccount.UsacAccountno,
                  pt => pt.PatrUsacAccountNoFrom,
                  (combined, pt) => new
                  {
                      combined.UserAccount.UsacUserEntityid,
                      pt.PatrTrxno,
                      pt.PatrCreatedOn,
                      pt.PatrDebet,
                      pt.PatrCredit,
                      pt.PatrUsacAccountNoFrom,
                      pt.PatrUsacAccountNoTo,
                      pt.PatrType,
                      pt.PatrInvoiceNo,
                      pt.PatrNotes,
                      pt.PatrTrxnoRev,
                  })
              .Where(x => x.UsacUserEntityid == parameter.UserEntityId)
              .Select(x => new PaymentTransaction
              {
                  PatrTrxno = x.PatrTrxno,
                  PatrCreatedOn = x.PatrCreatedOn,
                  PatrDebet = x.PatrDebet,
                  PatrCredit = x.PatrCredit,
                  PatrUsacAccountNoFrom = x.PatrUsacAccountNoFrom,
                  PatrUsacAccountNoTo = x.PatrUsacAccountNoTo,
                  PatrType = x.PatrType,
                  PatrInvoiceNo = x.PatrInvoiceNo,
                  PatrNotes = x.PatrNotes,
                  PatrTrxnoRev = x.PatrTrxnoRev
              })
              .ToList();

            var resultNEW = _dbContext.UserAccounts
              .Join(_dbContext.Users,
                  ua => ua.UsacUserEntityid,
                  us => us.UserEntityid,
                  (ua, us) => new { UserAccount = ua, User = us })
              .Join(_dbContext.PaymentTransactions,
                  combined => combined.UserAccount.UsacAccountno,
                  pt => pt.PatrUsacAccountNoTo,
                  (combined, pt) => new
                  {
                      combined.UserAccount.UsacUserEntityid,
                      pt.PatrTrxno,
                      pt.PatrCreatedOn,
                      pt.PatrDebet,
                      pt.PatrCredit,
                      pt.PatrUsacAccountNoFrom,
                      pt.PatrUsacAccountNoTo,
                      pt.PatrType,
                      pt.PatrInvoiceNo,
                      pt.PatrNotes,
                      pt.PatrTrxnoRev,
                  })
              .Where(x => x.UsacUserEntityid == parameter.UserEntityId)
              .Select(x => new PaymentTransaction
              {
                  PatrTrxno = x.PatrTrxno,
                  PatrCreatedOn = x.PatrCreatedOn,
                  PatrDebet = x.PatrDebet,
                  PatrCredit = x.PatrCredit,
                  PatrUsacAccountNoFrom = x.PatrUsacAccountNoFrom,
                  PatrUsacAccountNoTo = x.PatrUsacAccountNoTo,
                  PatrType = x.PatrType,
                  PatrInvoiceNo = x.PatrInvoiceNo,
                  PatrNotes = x.PatrNotes,
                  PatrTrxnoRev = x.PatrTrxnoRev
              })
              .ToList();

            transactions.AddRange(resultOLD);
            transactions.AddRange(resultNEW);

            return PagedList<PaymentTransaction>.ToPagedList(transactions.AsQueryable(), parameter.PageNumber, parameter.PageSize);
        }

        public async Task<PaymentTransaction> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(x => x.PatrTrxno.Equals(id), trackChanges).SingleOrDefaultAsync();

        }

        public int GetNexTrxSequence()
        {
            //int result = _dbContext.Database.ExecuteSqlRaw("SELECT NEXT VALUE FOR patr_id_seq;"); 
            var p = new SqlParameter("@result", System.Data.SqlDbType.Int);
            p.Direction = System.Data.ParameterDirection.Output;
            _dbContext.Database.ExecuteSqlRaw("set @result = next value for dbo.patr_id_seq", p);
            var nextVal = (int)p.Value;
            return nextVal;
        }

    }
}

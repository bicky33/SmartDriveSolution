using Domain.Entities.Payment;
using Domain.Repositories.Base;
using Domain.Repositories.Payment;
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

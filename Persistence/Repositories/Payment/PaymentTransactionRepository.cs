using Domain.Entities.Payment;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Payment
{
    public class PaymentTransactionRepository : RepositoryBase<PaymentTransaction>, IRepositoryEntityBase<PaymentTransaction>
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

        public async Task<PaymentTransaction> GetEntityById(int? id, bool trackChanges)
        {
            return await GetByCondition(x => x.PatrTrxno.Equals(id), trackChanges).SingleOrDefaultAsync();

        }
    }
}

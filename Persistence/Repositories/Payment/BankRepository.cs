using Domain.Entities.Payment;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Payment
{
    public class BankRepository : RepositoryBase<Bank>, IRepositoryEntityBase<Bank>
    {
        public BankRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Bank entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Bank entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Bank>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.BankEntityid).ToListAsync();
        }

        public async Task<Bank> GetEntityById(int? id, bool trackChanges)
        {
            return await GetByCondition(x => x.BankEntityid.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
 
}

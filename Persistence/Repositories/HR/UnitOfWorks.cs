using Domain.Repositories.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.HR
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly SmartDriveContext _dbContext;

        public UnitOfWorks(SmartDriveContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        
    }
}

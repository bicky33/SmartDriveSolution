using Domain.Repositories.Base;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly SmartDriveContext _dbContext;

        public UnitOfWorks(SmartDriveContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}

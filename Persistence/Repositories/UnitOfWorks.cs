using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly SmartDriveContext _dbContext;

        public UnitOfWorks(SmartDriveContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();

    }
}

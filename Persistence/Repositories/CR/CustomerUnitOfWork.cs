using Domain.Repositories.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CR
{
    public class CustomerUnitOfWork : ICustomerUnitOfWork
    {
        private readonly SmartDriveContext _smartDriveContext;

        public CustomerUnitOfWork(SmartDriveContext smartDriveContext)
        {
            _smartDriveContext = smartDriveContext;
        }

        public Task<int> SaveChangesAsync() =>
            _smartDriveContext.SaveChangesAsync();
    }
}

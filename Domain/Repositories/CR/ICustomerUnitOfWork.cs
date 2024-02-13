using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.CR
{
    public interface ICustomerUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}

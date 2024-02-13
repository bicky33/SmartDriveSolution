using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Base
{
    public interface IUnitOfWorks
    {
        Task<int> SaveChangesAsync();
    }
}

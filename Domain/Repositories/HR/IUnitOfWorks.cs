using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.HR
{
    public interface IUnitOfWorks
    {
        Task<int> SaveChangesAsync();
        //Task<string> SaveChangesDataAsync();
    }
}

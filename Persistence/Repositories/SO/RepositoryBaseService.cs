using Domain.Entities.SO;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.SO
{
    public class RepositoryBaseService<T> : RepositoryBase<ServiceOrder>
    {
        public RepositoryBaseService(SmartDriveContext dbContext) : base(dbContext)
        {
        }
    }
}

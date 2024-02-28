using Domain.Entities.Master;
using Domain.Repositories.Base;
using Domain.RequestFeatured;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Master
{
    public interface IRepositoryWithPaging<T> : IRepositoryEntityBase<T> where T : class
    {
        Task<PagedList<T>> GetWithPaging(EntityParameter entityParams, bool trackChanges);
    }
}

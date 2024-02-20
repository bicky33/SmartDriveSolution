using Domain.RequestFeatured;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Master
{
    public interface IServiceWithPaging<T> : IServiceEntityBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllWithPagingAsync(EntityParameter entityParams, bool trackChanges);
    }
}
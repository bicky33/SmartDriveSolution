using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.SO    
{
    public interface IRepositoryRelationSOBase<TEntity, TID> : IRepositorySOEntityBase<TEntity, TID> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllByRelation(string name, object value, bool trackChanges);
    }
}

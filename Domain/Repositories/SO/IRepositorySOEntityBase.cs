using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.SO    
{
    public interface IRepositorySOEntityBase<TEntity, TID> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllEntity(bool trackChanges);

        Task<TEntity> GetEntityById(TID id, bool trackChanges);

        void CreateEntity(TEntity entity);

        void DeleteEntity(TEntity entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.SO
{
    public interface IServiceEntityBase<TEntityDto, TEntityDtoCreate, TID> 
    {
        Task<IEnumerable<TEntityDto>> GetAllAsync(bool trackChanges);
        Task<TEntityDto> GetByIdAsync(TID id, bool trackChanges);
        Task<TEntityDtoCreate> CreateAsync(TEntityDtoCreate entity);
        Task<TEntityDtoCreate> UpdateAsync(TID id, TEntityDtoCreate entity);
        Task DeleteAsync(TID id);
    }
}

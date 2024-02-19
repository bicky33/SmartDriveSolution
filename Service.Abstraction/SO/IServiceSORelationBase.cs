using Contract.DTO.SO;
using Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.SO
{
    public interface IServiceSORelationBase<TEntityDto, TEntityDtoCreate, TID> : IServiceSOEntityBase<TEntityDto, TEntityDtoCreate, TID>
    {
        Task<IEnumerable<TEntityDto>> GetAllByRelation (string name, string value, bool trackChanges);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.SO
{
    public interface IServiceSORelationBase<TEntityDto> 
    {
        Task<TEntityDto> GetAllByRelation (bool trackChanges);
    }
}

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
        Task<TEntityDto> CreateServiceFeasibility(CreateServicePolisFeasibilityDto createServicePolisFeasibilityDto);
        Task<TEntityDto> CreateServicePolis(CreateServicePolisDto createServicePolisDto);
        Task<TEntityDto> CreateClaimPolis(CreateClaimPolisDto createClaimPolisDto);
        Task<TEntityDto> ClosePolis(int servId, string reason);
        Task<TEntityDto> SearchBySeroId(string seroId);
        Task<bool> AvailableServicePolis(int servId);
    }
}

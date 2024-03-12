using Contract.DTO.SO;

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
        Task<string> Debugging();
    }
}

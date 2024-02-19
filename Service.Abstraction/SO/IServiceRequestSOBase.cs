using Contract.DTO.SO;

namespace Service.Abstraction.SO
{
    public interface IServiceRequestSOBase
    { 
        public Task CreateServicePolisFeasibility(CreateServicePolisFeasibilityDto createServicePolisDto);
        public Task CreateServicePolis(CreateServicePolisDto createServicePolisDto);
        public Task ClosePolis(int servId, string reason);
        public Task CreateClaimPolis(CreateClaimPolisDto createClaimPolisDto);
        public void Debugging();
    }
}

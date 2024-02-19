using Contract.DTO.SO;

namespace Service.Abstraction.SO
{
    public interface IServiceRequestSOBase
    { 
        public Task CreateServicePolis(CreateServicePolisDto createServicePolisDto);
        public void Debugging();
    }
}

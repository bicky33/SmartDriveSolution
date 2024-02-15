using Contract.DTO.Master;

namespace Service.Abstraction.Base.Master
{
    public interface IServiceTemplateServiceTask : IServiceEntityBase<TemplateServiceTaskResponse>
    {

        Task<IEnumerable<TemplateServiceTaskResponse>> GetAllTestaAsync(int id,bool trackChanges);
        Task<TemplateServiceTaskResponse> GetTestaByTestaTetyIdAsync(int id, bool trackChanges);
    }
}
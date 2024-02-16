using Contract.DTO.Master;
using Service.Abstraction.Base;

namespace Service.Abstraction.Master
{
    public interface IServiceTemplateServiceTask : IServiceEntityBase<TemplateServiceTaskResponse>
    {

        Task<IEnumerable<TemplateServiceTaskResponse>> GetAllTestaAsync(int id, bool trackChanges);
        Task<TemplateServiceTaskResponse> GetTestaByTestaTetyIdAsync(int id, bool trackChanges);
    }
}
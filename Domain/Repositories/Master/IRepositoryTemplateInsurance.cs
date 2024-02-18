using Domain.Entities.Master;
using Domain.Repositories.Base;

namespace Domain.Repositories.Master
{
    public interface IRepositoryTemplateServiceTask : IRepositoryEntityBase<TemplateServiceTask>
    {
        Task<IEnumerable<TemplateServiceTask>> GetAllTestaByTestaTetyID(int id, bool trackChanges);
        Task<TemplateServiceTask> GetTestaByTestaTetyID(int id, bool trackChanges);
    }
}
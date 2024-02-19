using Domain.Entities.Master;
using Domain.Repositories.Base;

namespace Domain.Repositories.Master
{
    public interface IRepositoryTemplateInsurancePremi : IRepositoryEntityBase<TemplateInsurancePremi>
    {
        Task<TemplateInsurancePremi> GetTemiByCateIDIntyNameZoneID(int cateId,string intyName,int zoneId,bool trackChanges);
    }
}
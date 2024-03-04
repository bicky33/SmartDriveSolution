using Contract.DTO.Master;
using Service.Abstraction.Base;

namespace Service.Abstraction.Master
{
    public interface IServiceTemplateInsurancePremi : IServiceEntityBase<TemplateInsurancePremiResponse>
    {

        Task<TemplateInsurancePremiResponse> GetTemiByCateIdIntyNameZoneIdAsync(int cateId,string intyName, int zoneId, bool trackChanges);
    }
}
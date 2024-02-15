using Contract.DTO.Master;
using Service.Abstraction.Base;
using Service.Abstraction.Base.Master;

namespace Service.Abstraction.Master
{
    public interface IServiceManagerMaster
    {
        IServiceEntityBase<CarBrandResponse> CarBrandService { get; }
        IServiceEntityBase<CarModelResponse> CarModelService { get; }
        IServiceEntityBase<CarSeriesResponse> CarSeriesService { get; }
        IServiceEntityBase<CategoryResponse> CategoryService { get; }
        IServiceEntityBase<ZoneResponse> ZoneService { get; }
        IServiceEntityBase<ProvinsiResponse> ProvinsiService { get; }
        IServiceEntityBase<CityResponse> CityService { get; }
        IServiceEntityBaseMaster<InsuranceTypeResponse> InsuranceTypeService { get; }
        IServiceEntityBaseMaster<RegionPLatResponse> RegionPlatService { get; }
        IServiceEntityBaseMaster<AreaWorkgroupResponse> AreaWorkgroupService { get; }
        IServiceEntityBase<TemplateTypeResponse> TemplateTypeService { get; }
        IServiceTemplateServiceTask TemplateServiceTaskService { get; }
        IServiceEntityBase<TemplateTaskWorkorderResponse> TemplateTaskWorkorderService { get; }
    }
}
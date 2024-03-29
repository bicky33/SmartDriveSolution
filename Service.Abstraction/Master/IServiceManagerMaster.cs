﻿using Contract.DTO.Master;
using Service.Abstraction.Base;

namespace Service.Abstraction.Master
{
    public interface IServiceManagerMaster
    {
        IServiceEntityBase<CarBrandResponse> CarBrandService { get; }
        IServiceEntityBase<CarModelResponse> CarModelService { get; }
        IServiceEntityBase<CarSeriesResponse> CarSeriesService { get; }
        IServiceEntityBase<CategoryResponse> CategoryService { get; }
        IServiceEntityBase<ZoneResponse> ZoneService { get; }
        IServiceWithPaging<ProvinsiResponse> ProvinsiService { get; }
        IServiceEntityBase<CityResponse> CityService { get; }
        IServiceEntityBaseMaster<InsuranceTypeResponse> InsuranceTypeService { get; }
        IServiceEntityBaseMaster<RegionPlatResponse> RegionPlatService { get; }
        IServiceEntityBaseMaster<AreaWorkgroupResponse> AreaWorkgroupService { get; }
        IServiceEntityBase<TemplateTypeResponse> TemplateTypeService { get; }
        IServiceTemplateServiceTask TemplateServiceTaskService { get; }
        IServiceEntityBase<TemplateTaskWorkorderResponse> TemplateTaskWorkorderService { get; }
        IServiceEntityBase<TemplateInsurancePremiResponse> TemplateInsurancePremiService { get; }
    }
}
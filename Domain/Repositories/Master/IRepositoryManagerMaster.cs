using Domain.Entities.Master;
using Domain.Repositories.Base;

namespace Domain.Repositories.Master
{
    public interface IRepositoryManagerMaster
    {
        IRepositoryEntityBase<CarBrand> CarBrandRepository { get; }
        IRepositoryEntityBase<CarModel> CarModelRepository { get; }
        IRepositoryEntityBase<CarSeries> CarSeriesRepository { get; }
        IRepositoryEntityBase<Category> CategoryRepository { get; }
        IRepositoryEntityBaseMaster<InsuranceType> InsuranceTypeRepository { get; }
        IRepositoryEntityBase<Zone> ZoneRepository { get; }
        IRepositoryEntityBase<Provinsi> ProvinsiRepository { get; }
        IRepositoryEntityBase<City> CityRepository { get; }
        IRepositoryEntityBaseMaster<RegionPlat> RegionPlatRepository { get; }
        IRepositoryEntityBaseMaster<AreaWorkgroup> AreaWorkgroupRepository { get; }
        IRepositoryEntityBase<TemplateType> TemplateTypeRepository { get; }
        IRepositoryTemplateServiceTask TemplateServiceTaskRepository { get; }
        IRepositoryEntityBase<TemplateTaskWorkorder> TemplateTaskWorkorderRepository { get; }
        IRepositoryTemplateInsurancePremi TemplateInsurancePremiRepository { get; }
        IUnitOfWorks UnitOfWork { get; }
    }
}
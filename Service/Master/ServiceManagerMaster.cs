using Contract.DTO.Master;
using Domain.Repositories.Master;
using Service.Abstraction.Base;
using Service.Abstraction.Master;

namespace Service.Master
{
    public class ServiceManagerMaster : IServiceManagerMaster
    {
        private readonly Lazy<IServiceEntityBase<CarBrandResponse>> _carBrandService;
        private readonly Lazy<IServiceEntityBase<CarModelResponse>> _carModelService;
        private readonly Lazy<IServiceEntityBase<CarSeriesResponse>> _carSeriesService;
        private readonly Lazy<IServiceEntityBase<CategoryResponse>> _categoryService;
        private readonly Lazy<IServiceEntityBase<ZoneResponse>> _zoneService;
        private readonly Lazy<IServiceEntityBase<ProvinsiResponse>> _provinsiService;
        private readonly Lazy<IServiceEntityBase<CityResponse>> _cityService;
        private readonly Lazy<IServiceEntityBaseMaster<InsuranceTypeResponse>> _insuranceTypeService;
        private readonly Lazy<IServiceEntityBaseMaster<RegionPLatResponse>> _regionPlatService;
        private readonly Lazy<IServiceEntityBaseMaster<AreaWorkgroupResponse>> _areaWorkgroupService;

        public ServiceManagerMaster(IRepositoryManagerMaster repositoryManager)
        {
            _carBrandService = new Lazy<IServiceEntityBase<CarBrandResponse>>(() => new CarBrandService(repositoryManager));
            _carModelService= new Lazy<IServiceEntityBase<CarModelResponse>>(() => new CarModelService(repositoryManager));
            _carSeriesService = new Lazy<IServiceEntityBase<CarSeriesResponse>>(() => new CarSeriesService(repositoryManager));
            _categoryService = new Lazy<IServiceEntityBase<CategoryResponse>>(() => new CategoryService(repositoryManager));
            _zoneService = new Lazy<IServiceEntityBase<ZoneResponse>>(() => new ZoneService(repositoryManager));
            _provinsiService = new Lazy<IServiceEntityBase<ProvinsiResponse>>(() => new ProvinsiService(repositoryManager));
            _cityService = new Lazy<IServiceEntityBase<CityResponse>>(() => new CityService(repositoryManager));
            _insuranceTypeService = new Lazy<IServiceEntityBaseMaster<InsuranceTypeResponse>>(() => new InsuranceTypeService(repositoryManager));
            _regionPlatService = new Lazy<IServiceEntityBaseMaster<RegionPLatResponse>>(() => new RegionPlatService(repositoryManager));
            _areaWorkgroupService = new Lazy<IServiceEntityBaseMaster<AreaWorkgroupResponse>>(() => new AreaWorkgroupService(repositoryManager));
        }

        public IServiceEntityBase<CarBrandResponse> CarBrandService => _carBrandService.Value;

        public IServiceEntityBase<CarModelResponse> CarModelService => _carModelService.Value;

        public IServiceEntityBase<CarSeriesResponse> CarSeriesService => _carSeriesService.Value;

        public IServiceEntityBase<CategoryResponse> CategoryService => _categoryService.Value;

        public IServiceEntityBase<ZoneResponse> ZoneService => _zoneService.Value;

        public IServiceEntityBase<ProvinsiResponse> ProvinsiService => _provinsiService.Value;

        public IServiceEntityBase<CityResponse> CityService => _cityService.Value;
        public IServiceEntityBaseMaster<InsuranceTypeResponse> InsuranceTypeService=> _insuranceTypeService.Value;

        public IServiceEntityBaseMaster<RegionPLatResponse> RegionPlatService => _regionPlatService.Value;

        public IServiceEntityBaseMaster<AreaWorkgroupResponse> AreaWorkgroupService => _areaWorkgroupService.Value;
    }
}
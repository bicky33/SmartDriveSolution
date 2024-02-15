using Domain.Entities.Master;
using Domain.Repositories.Base;
using Domain.Repositories.Master;

namespace Persistence.Repositories.Master
{
    public class RepositoryManagerMaster : IRepositoryManagerMaster
    {
        private readonly Lazy<IUnitOfWorks> _unitOfWorks;
        private readonly Lazy<IRepositoryEntityBase<CarBrand>> _carBrand;
        private readonly Lazy<IRepositoryEntityBase<CarModel>> _carModel;
        private readonly Lazy<IRepositoryEntityBase<CarSeries>> _carSeries;
        private readonly Lazy<IRepositoryEntityBase<Category>> _category;
        private readonly Lazy<IRepositoryEntityBase<Zone>> _zone;
        private readonly Lazy<IRepositoryEntityBase<Provinsi>> _provinsi;
        private readonly Lazy<IRepositoryEntityBase<City>> _city;
        private readonly Lazy<IRepositoryEntityBaseMaster<InsuranceType>> _insuranceType;
        private readonly Lazy<IRepositoryEntityBaseMaster<RegionPlat>> _regionPlat;
        private readonly Lazy<IRepositoryEntityBaseMaster<AreaWorkgroup>> _areaWorkgroup;
        private readonly Lazy<IRepositoryEntityBase<TemplateType>> _templateType;
        private readonly Lazy<IRepositoryTemplateServiceTask> _templateServiceTask;
        private readonly Lazy<IRepositoryEntityBase<TemplateTaskWorkorder>> _templateTaskWorkorder;
        private readonly Lazy<IRepositoryEntityBase<TemplateInsurancePremi>> _templateInsurancePremi;

        public RepositoryManagerMaster(SmartDriveContext _context)
        {
            _unitOfWorks = new Lazy<IUnitOfWorks>(() => new UnitOfWorks(_context));
            _carBrand = new Lazy<IRepositoryEntityBase<CarBrand>>(() => new CarBrandRepository(_context));
            _carModel = new Lazy<IRepositoryEntityBase<CarModel>>(() => new CarModelRepository(_context));
            _carSeries = new Lazy<IRepositoryEntityBase<CarSeries>>(() => new CarSeriesRepository(_context));
            _category = new Lazy<IRepositoryEntityBase<Category>>(() => new CategoryRepository(_context));
            _zone = new Lazy<IRepositoryEntityBase<Zone>>(() => new ZoneRepository(_context));
            _provinsi = new Lazy<IRepositoryEntityBase<Provinsi>>(() => new ProvinsiRepository(_context));
            _city = new Lazy<IRepositoryEntityBase<City>>(() => new CityRepository(_context));
            _insuranceType = new Lazy<IRepositoryEntityBaseMaster<InsuranceType>>(() => new InsuranceTypeRepository(_context));
            _regionPlat = new Lazy<IRepositoryEntityBaseMaster<RegionPlat>>(() => new RegionPlatRepository(_context));
            _areaWorkgroup = new Lazy<IRepositoryEntityBaseMaster<AreaWorkgroup>>(() => new AreaWorkgroupRepository(_context));
            _templateType = new Lazy<IRepositoryEntityBase<TemplateType>>(() => new TemplateTypeRepository(_context));
            _templateServiceTask = new Lazy<IRepositoryTemplateServiceTask>(() => new TemplateServiceTaskRepository(_context));
            _templateTaskWorkorder = new Lazy<IRepositoryEntityBase<TemplateTaskWorkorder>>(() => new TemplateTaskWorkorderRepository(_context));
            _templateInsurancePremi = new Lazy<IRepositoryEntityBase<TemplateInsurancePremi>>(() => new TemplateInsurancePremiRespository(_context));
        }

        public IUnitOfWorks UnitOfWork => _unitOfWorks.Value;

        public IRepositoryEntityBase<CarBrand> CarBrandRepository => _carBrand.Value;

        public IRepositoryEntityBase<CarModel> CarModelRepository => _carModel.Value;

        public IRepositoryEntityBase<CarSeries> CarSeriesRepository => _carSeries.Value;

        public IRepositoryEntityBase<Category> CategoryRepository => _category.Value;
        public IRepositoryEntityBase<Zone> ZoneRepository => _zone.Value;

        public IRepositoryEntityBase<Provinsi> ProvinsiRepository => _provinsi.Value;

        public IRepositoryEntityBase<City> CityRepository => _city.Value;
        public IRepositoryEntityBaseMaster<InsuranceType> InsuranceTypeRepository => _insuranceType.Value;

        public IRepositoryEntityBaseMaster<RegionPlat> RegionPlatRepository => _regionPlat.Value;

        public IRepositoryEntityBaseMaster<AreaWorkgroup> AreaWorkgroupRepository => _areaWorkgroup.Value;

        public IRepositoryEntityBase<TemplateType> TemplateTypeRepository => _templateType.Value;

        public IRepositoryTemplateServiceTask TemplateServiceTaskRepository => _templateServiceTask.Value;

        public IRepositoryEntityBase<TemplateTaskWorkorder> TemplateTaskWorkorderRepository => _templateTaskWorkorder.Value;

        public IRepositoryEntityBase<TemplateInsurancePremi> TemplateInsurancePremiRepository => _templateInsurancePremi.Value;
    }
}
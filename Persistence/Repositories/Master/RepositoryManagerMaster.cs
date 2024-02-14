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

        public RepositoryManagerMaster(SmartDriveContext _context)
        {
            _unitOfWorks = new Lazy<IUnitOfWorks>(() => new UnitOfWorks(_context));
            _carBrand = new Lazy<IRepositoryEntityBase<CarBrand>>(() => new CarBrandRepository(_context));
            _carModel = new Lazy<IRepositoryEntityBase<CarModel>>(() => new CarModelRepository(_context));
            _carSeries = new Lazy<IRepositoryEntityBase<CarSeries>>(() => new CarSeriesRepository(_context));
            _category = new Lazy<IRepositoryEntityBase<Category>>(() => new CategoryRepository(_context));
        }

        public IUnitOfWorks UnitOfWork => _unitOfWorks.Value;

        public IRepositoryEntityBase<CarBrand> CarBrandRepository => _carBrand.Value;

        public IRepositoryEntityBase<CarModel> CarModelRepository => _carModel.Value;

        public IRepositoryEntityBase<CarSeries> CarSeriesRepository => _carSeries.Value;

        public IRepositoryEntityBase<Category> CategoryRepository => _category.Value;
    }
}
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
        IUnitOfWorks UnitOfWork { get; }
    }
}
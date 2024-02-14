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

        public ServiceManagerMaster(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _carBrandService = new Lazy<IServiceEntityBase<CarBrandResponse>>(() => new CarBrandService(repositoryManagerMaster));
            _carModelService= new Lazy<IServiceEntityBase<CarModelResponse>>(() => new CarModelService(repositoryManagerMaster));
            _carSeriesService = new Lazy<IServiceEntityBase<CarSeriesResponse>>(() => new CarSeriesService(repositoryManagerMaster));
            _categoryService = new Lazy<IServiceEntityBase<CategoryResponse>>(() => new CategoryService(repositoryManagerMaster));
        }

        public IServiceEntityBase<CarBrandResponse> CarBrandService => _carBrandService.Value;

        public IServiceEntityBase<CarModelResponse> CarModelService => _carModelService.Value;

        public IServiceEntityBase<CarSeriesResponse> CarSeriesService => _carSeriesService.Value;

        public IServiceEntityBase<CategoryResponse> CategoryService => _categoryService.Value;
    }
}
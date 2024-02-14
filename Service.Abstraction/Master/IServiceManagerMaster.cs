using Contract.DTO.Master;
using Service.Abstraction.Base;

namespace Service.Abstraction.Master
{
    public interface IServiceManagerMaster
    {
        IServiceEntityBase<CarBrandResponse> CarBrandService { get; }
        IServiceEntityBase<CarModelResponse> CarModelService { get; }
        IServiceEntityBase<CarSeriesResponse> CarSeriesService { get; }
        IServiceEntityBase<CategoryResponse> CategoryService { get; }
    }
}
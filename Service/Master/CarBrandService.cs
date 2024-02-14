using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Master
{
    public class CarBrandService : IServiceEntityBase<CarBrandResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public CarBrandService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<CarBrandResponse> CreateAsync(CarBrandResponse entity)
        {
            var carBrand = entity.Adapt<CarBrand>();
            _repositoryManagerMaster.CarBrandRepository.CreateEntity(carBrand);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return carBrand.Adapt<CarBrandResponse>();
        }

        public async Task DeleteAsync(int id)
        {
            var carBrand = await _repositoryManagerMaster.CarBrandRepository.GetEntityById(id, false);
            if (carBrand == null)
            {
                throw new EntityNotFoundException(id);
            }
            _repositoryManagerMaster.CarBrandRepository.DeleteEntity(carBrand);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarBrandResponse>> GetAllAsync(bool trackChanges)
        {
            var carBrands = await _repositoryManagerMaster.CarBrandRepository.GetAllEntity(false);
            var carBrandsResponse = carBrands.Adapt<IEnumerable<CarBrandResponse>>();
            return carBrandsResponse;
        }

        public async Task<CarBrandResponse> GetByIdAsync(int id, bool trackChanges)
        {
            var carBrand = await _repositoryManagerMaster.CarBrandRepository.GetEntityById(id, false);
            if (carBrand == null)
            {
                throw new EntityNotFoundException(id);
            }
            var carBrandResponse = carBrand.Adapt<CarBrandResponse>();
            return carBrandResponse;
        }

        public async Task<CarBrandResponse> UpdateAsync(int id, CarBrandResponse entity)
        {
            var carBrand = await _repositoryManagerMaster.CarBrandRepository.GetEntityById(id, true);
            if (carBrand == null)
            {
                throw new EntityNotFoundException(id);
            }

            carBrand.CabrId = entity.CabrId;
            carBrand.CabrName = entity.CabrName;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return carBrand.Adapt<CarBrandResponse>();
        }
    }
}
using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Master
{
    public class CarModelService : IServiceEntityBase<CarModelResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public CarModelService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<CarModelResponse> CreateAsync(CarModelResponse entity)
        {
            var carModel = entity.Adapt<CarModel>();
            _repositoryManagerMaster.CarModelRepository.CreateEntity(carModel);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return carModel.Adapt<CarModelResponse>();
        }

        public async Task DeleteAsync(int id)
        {
            var carModel = await _repositoryManagerMaster.CarModelRepository.GetEntityById(id, false);
            if (carModel == null)
            {
                throw new EntityNotFoundException(id);
            }
            _repositoryManagerMaster.CarModelRepository.DeleteEntity(carModel);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarModelResponse>> GetAllAsync(bool trackChanges)
        {
            var carModels = await _repositoryManagerMaster.CarModelRepository.GetAllEntity(false);
            var carModelsResponse = carModels.Adapt<IEnumerable<CarModelResponse>>();
            return carModelsResponse;
        }

        public async Task<CarModelResponse> GetByIdAsync(int id, bool trackChanges)
        {
            var carModel = await _repositoryManagerMaster.CarModelRepository.GetEntityById(id, false);
            if (carModel == null)
            {
                throw new EntityNotFoundException(id);
            }
            var carModelResponse = carModel.Adapt<CarModelResponse>();
            return carModelResponse;
        }

        public async Task<CarModelResponse> UpdateAsync(int id, CarModelResponse entity)
        {
            var carModel = await _repositoryManagerMaster.CarModelRepository.GetEntityById(id, true);
            if (carModel == null)
            {
                throw new EntityNotFoundException(id);
            }

            carModel.CarmName = entity.CarmName;
            carModel.CarmCabrId = entity.CarmCabrId;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return carModel.Adapt<CarModelResponse>();
        }
    }
}
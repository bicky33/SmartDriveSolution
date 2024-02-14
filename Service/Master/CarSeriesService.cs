using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Master
{
    public class CarSeriesService : IServiceEntityBase<CarSeriesResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public CarSeriesService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<CarSeriesResponse> CreateAsync(CarSeriesResponse entity)
        {
            var carSeries = entity.Adapt<CarSeries>();
            _repositoryManagerMaster.CarSeriesRepository.CreateEntity(carSeries);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return carSeries.Adapt<CarSeriesResponse>();
        }

        public async Task DeleteAsync(int id)
        {
            var carSeries = await _repositoryManagerMaster.CarSeriesRepository.GetEntityById(id, false);
            if (carSeries == null)
            {
                throw new EntityNotFoundException(id);
            }
            _repositoryManagerMaster.CarSeriesRepository.DeleteEntity(carSeries);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarSeriesResponse>> GetAllAsync(bool trackChanges)
        {
            var carSeries = await _repositoryManagerMaster.CarSeriesRepository.GetAllEntity(false);
            var carSeriesResponse = carSeries.Adapt<IEnumerable<CarSeriesResponse>>();
            return carSeriesResponse;
        }

        public async Task<CarSeriesResponse> GetByIdAsync(int id, bool trackChanges)
        {
            var carSeries = await _repositoryManagerMaster.CarSeriesRepository.GetEntityById(id, false);
            if (carSeries == null)
            {
                throw new EntityNotFoundException(id);
            }
            var carSeriesResponse = carSeries.Adapt<CarSeriesResponse>();
            return carSeriesResponse;
        }

        public async Task<CarSeriesResponse> UpdateAsync(int id, CarSeriesResponse entity)
        {
            var carSeries = await _repositoryManagerMaster.CarSeriesRepository.GetEntityById(id, true);
            if (carSeries == null)
            {
                throw new EntityNotFoundException(id);
            }

            carSeries.CarsName = entity.CarsName;
            carSeries.CarsPassenger = entity.CarsPassenger;
            carSeries.CarsCarmId = entity.CarsCarmId;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return carSeries.Adapt<CarSeriesResponse>();
        }
    }
}
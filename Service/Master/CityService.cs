using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Master
{
    public class CityService : IServiceEntityBase<CityResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public CityService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<CityResponse> CreateAsync(CityResponse entity)
        {
            var city = entity.Adapt<City>();
            _repositoryManagerMaster.CityRepository.CreateEntity(city);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return city.Adapt<CityResponse>();
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _repositoryManagerMaster.CityRepository.GetEntityById(id, false);
            if (city == null)
            {
                throw new EntityNotFoundException(id, nameof(city));
            }
            _repositoryManagerMaster.CityRepository.DeleteEntity(city);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CityResponse>> GetAllAsync(bool trackChanges)
        {
            var categories = await _repositoryManagerMaster.CityRepository.GetAllEntity(false);
            var categoriesResponse = categories.Adapt<IEnumerable<CityResponse>>();
            return categoriesResponse;
        }

        public async Task<CityResponse> GetByIdAsync(int id, bool trackChanges)
        {
            var city = await _repositoryManagerMaster.CityRepository.GetEntityById(id, false);
            if (city == null)
            {
                throw new EntityNotFoundException(id, nameof(city));
            }
            var cityResponse = city.Adapt<CityResponse>();
            return cityResponse;
        }

        public async Task UpdateAsync(int id, CityResponse entity)
        {
            var city = await _repositoryManagerMaster.CityRepository.GetEntityById(id, true);
            if (city == null)
            {
                throw new EntityNotFoundException(id, nameof(entity));
            }
            city.CityId = entity.CityId;
            city.CityName = entity.CityName;
            city.CityProvId = entity.CityProvId;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }
    }
}
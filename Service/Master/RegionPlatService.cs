using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Master;

namespace Service.Master
{
    public class RegionPlatService : IServiceEntityBaseMaster<RegionPlatResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public RegionPlatService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<RegionPlatResponse> CreateAsyncMaster(RegionPlatResponse entity)
        {
            var regionPlat = entity.Adapt<RegionPlat>();
            _repositoryManagerMaster.RegionPlatRepository.CreateEntityMaster(regionPlat);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return regionPlat.Adapt<RegionPlatResponse>();
        }

        public async Task DeleteAsyncMaster(string name)
        {
            var regionPlat = await _repositoryManagerMaster.RegionPlatRepository.GetEntityByNameMaster(name, false);
            if (regionPlat == null)
            {
                throw new EntityNotFoundException(name, nameof(regionPlat));
            }
            _repositoryManagerMaster.RegionPlatRepository.DeleteEntityMaster(regionPlat);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<RegionPlatResponse>> GetAllAsyncMaster(bool trackChanges)
        {
            var regionPlats = await _repositoryManagerMaster.RegionPlatRepository.GetAllEntityMaster(false);
            var regionPlatsResponse = regionPlats.Adapt<IEnumerable<RegionPlatResponse>>();
            return regionPlatsResponse;
        }

        public async Task<RegionPlatResponse> GetByNameAsyncMaster(string name, bool trackChanges)
        {
            var regionPlat = await _repositoryManagerMaster.RegionPlatRepository.GetEntityByNameMaster(name, false);
            if (regionPlat == null)
            {

                throw new EntityNotFoundException(name, nameof(regionPlat));
            }
            var regionPlatResponse = regionPlat.Adapt<RegionPlatResponse>();
            return regionPlatResponse;
        }

        public async Task UpdateAsyncMaster(string name, RegionPlatResponse entity)
        {
            var regionPlat = await _repositoryManagerMaster.RegionPlatRepository.GetEntityByNameMaster(name, true);
            if (regionPlat == null)
            {

                throw new EntityNotFoundException(name, nameof(entity));
            }
            regionPlat.RegpName = entity.RegpName;
            regionPlat.RegpDesc = entity.RegpDesc;
            regionPlat.RegpProvId = entity.RegpProvId;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }
    }
}
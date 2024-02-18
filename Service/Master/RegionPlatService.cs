using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Master;

namespace Service.Master
{
    public class RegionPlatService : IServiceEntityBaseMaster<RegionPLatResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public RegionPlatService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<RegionPLatResponse> CreateAsyncMaster(RegionPLatResponse entity)
        {
            var regionPlat = entity.Adapt<RegionPlat>();
            _repositoryManagerMaster.RegionPlatRepository.CreateEntityMaster(regionPlat);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return regionPlat.Adapt<RegionPLatResponse>();
        }

        public async Task DeleteAsyncMaster(string name)
        {
            var regionPlat = await _repositoryManagerMaster.RegionPlatRepository.GetEntityByNameMaster(name, false);
            if (regionPlat == null)
            {
                //throw new EntityNotFoundException(name);
                throw new Exception($"Data {name} Not Found");
            }
            _repositoryManagerMaster.RegionPlatRepository.DeleteEntityMaster(regionPlat);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<RegionPLatResponse>> GetAllAsyncMaster(bool trackChanges)
        {
            var regionPlats = await _repositoryManagerMaster.RegionPlatRepository.GetAllEntityMaster(false);
            var regionPlatsResponse = regionPlats.Adapt<IEnumerable<RegionPLatResponse>>();
            return regionPlatsResponse;
        }

        public async Task<RegionPLatResponse> GetByNameAsyncMaster(string name, bool trackChanges)
        {
            var regionPlat = await _repositoryManagerMaster.RegionPlatRepository.GetEntityByNameMaster(name, false);
            if (regionPlat == null)
            {
                //throw new EntityNotFoundException(name);
                throw new Exception($"Data {name} Not Found");
            }
            var regionPlatResponse = regionPlat.Adapt<RegionPLatResponse>();
            return regionPlatResponse;
        }

        public async Task<RegionPLatResponse> UpdateAsyncMaster(string name, RegionPLatResponse entity)
        {
            var regionPlat = await _repositoryManagerMaster.RegionPlatRepository.GetEntityByNameMaster(name, true);
            if (regionPlat == null)
            {
                //throw new EntityNotFoundException(id);
                throw new Exception($"Data {name} Not Found");
            }
            regionPlat.RegpName = entity.RegpName;
            regionPlat.RegpDesc = entity.RegpDesc;
            regionPlat.RegpProvId = entity.RegpProvId;  

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
            return regionPlat.Adapt<RegionPLatResponse>();
        }
    }
}
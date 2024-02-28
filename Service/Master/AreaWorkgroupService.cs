using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Master;

namespace Service.Master
{
    public class AreaWorkgroupService : IServiceEntityBaseMaster<AreaWorkgroupResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public AreaWorkgroupService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<AreaWorkgroupResponse> CreateAsyncMaster(AreaWorkgroupResponse entity)
        {
            var areaWorkgroup = entity.Adapt<AreaWorkgroup>();
            _repositoryManagerMaster.AreaWorkgroupRepository.CreateEntityMaster(areaWorkgroup);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return areaWorkgroup.Adapt<AreaWorkgroupResponse>();
        }

        public async Task DeleteAsyncMaster(string name)
        {
            var areaWorkgroup = await _repositoryManagerMaster.AreaWorkgroupRepository.GetEntityByNameMaster(name, false);
            if (areaWorkgroup == null)
            {
                throw new EntityNotFoundException(name, nameof(areaWorkgroup));
            }
            _repositoryManagerMaster.AreaWorkgroupRepository.DeleteEntityMaster(areaWorkgroup);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<AreaWorkgroupResponse>> GetAllAsyncMaster(bool trackChanges)
        {
            var areaWorkgroups = await _repositoryManagerMaster.AreaWorkgroupRepository.GetAllEntityMaster(false);
            var areaWorkgroupsResponse = areaWorkgroups.Adapt<IEnumerable<AreaWorkgroupResponse>>();
            return areaWorkgroupsResponse;
        }

        public async Task<AreaWorkgroupResponse> GetByNameAsyncMaster(string name, bool trackChanges)
        {
            var areaWorkgroup = await _repositoryManagerMaster.AreaWorkgroupRepository.GetEntityByNameMaster(name, false);
            if (areaWorkgroup == null)
            {
                throw new EntityNotFoundException(name, nameof(areaWorkgroup));
            }
            var areaWorkgroupResponse = areaWorkgroup.Adapt<AreaWorkgroupResponse>();
            return areaWorkgroupResponse;
        }

        public async Task UpdateAsyncMaster(string name, AreaWorkgroupResponse entity)
        {
            var areaWorkgroup = await _repositoryManagerMaster.AreaWorkgroupRepository.GetEntityByNameMaster(name, true);
            if (areaWorkgroup == null)
            {
                throw new EntityNotFoundException(name, nameof(entity));
            }
            areaWorkgroup.ArwgCode = entity.ArwgCode;
            areaWorkgroup.ArwgDesc = entity.ArwgDesc;
            areaWorkgroup.ArwgCityId = entity.ArwgCityId;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }
    }
}
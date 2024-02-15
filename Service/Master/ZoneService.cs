using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Master
{
    public class ZoneService : IServiceEntityBase<ZoneResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public ZoneService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<ZoneResponse> CreateAsync(ZoneResponse entity)
        {
            var zone = entity.Adapt<Zone>();
            _repositoryManagerMaster.ZoneRepository.CreateEntity(zone);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return zone.Adapt<ZoneResponse>();
        }

        public async Task DeleteAsync(int id)
        {
            var zone = await _repositoryManagerMaster.ZoneRepository.GetEntityById(id, false);
            if (zone == null)
            {
                throw new EntityNotFoundException(id, nameof(zone));
            }
            _repositoryManagerMaster.ZoneRepository.DeleteEntity(zone);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ZoneResponse>> GetAllAsync(bool trackChanges)
        {
            var zones = await _repositoryManagerMaster.ZoneRepository.GetAllEntity(false);
            var zonesResponse = zones.Adapt<IEnumerable<ZoneResponse>>();
            return zonesResponse;
        }

        public async Task<ZoneResponse> GetByIdAsync(int id, bool trackChanges)
        {
            var zone = await _repositoryManagerMaster.ZoneRepository.GetEntityById(id, false);
            if (zone == null)
            {
                throw new EntityNotFoundException(id, nameof(zone));
            }
            var zoneResponse = zone.Adapt<ZoneResponse>();
            return zoneResponse;
        }

        public async Task UpdateAsync(int id, ZoneResponse entity)
        {
            var zone = await _repositoryManagerMaster.ZoneRepository.GetEntityById(id, true);
            if (zone == null)
            {
                throw new EntityNotFoundException(id, nameof(entity));
            }
            zone.ZonesId = entity.ZonesId;
            zone.ZonesName = entity.ZonesName;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }
    }
}
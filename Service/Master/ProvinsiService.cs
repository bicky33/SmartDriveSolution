using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Master
{
    public class ProvinsiService : IServiceEntityBase<ProvinsiResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public ProvinsiService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<ProvinsiResponse> CreateAsync(ProvinsiResponse entity)
        {
            var provinsi = entity.Adapt<Provinsi>();
            _repositoryManagerMaster.ProvinsiRepository.CreateEntity(provinsi);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return provinsi.Adapt<ProvinsiResponse>();
        }

        public async Task DeleteAsync(int id)
        {
            var provinsi = await _repositoryManagerMaster.ProvinsiRepository.GetEntityById(id, false);
            if (provinsi == null)
            {
                throw new EntityNotFoundException(id);
            }
            _repositoryManagerMaster.ProvinsiRepository.DeleteEntity(provinsi);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProvinsiResponse>> GetAllAsync(bool trackChanges)
        {
            var categories = await _repositoryManagerMaster.ProvinsiRepository.GetAllEntity(false);
            var categoriesResponse = categories.Adapt<IEnumerable<ProvinsiResponse>>();
            return categoriesResponse;
        }

        public async Task<ProvinsiResponse> GetByIdAsync(int id, bool trackChanges)
        {
            var provinsi = await _repositoryManagerMaster.ProvinsiRepository.GetEntityById(id, false);
            if (provinsi == null)
            {
                throw new EntityNotFoundException(id);
            }
            var provinsiResponse = provinsi.Adapt<ProvinsiResponse>();
            return provinsiResponse;
        }

        public async Task<ProvinsiResponse> UpdateAsync(int id, ProvinsiResponse entity)
        {
            var provinsi = await _repositoryManagerMaster.ProvinsiRepository.GetEntityById(id, true);
            if (provinsi == null)
            {
                throw new EntityNotFoundException(id);
            }
            provinsi.ProvId = entity.ProvId;
            provinsi.ProvName = entity.ProvName;
            provinsi.ProvZonesId = entity.ProvZonesId;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
            return provinsi.Adapt<ProvinsiResponse>();
        }
    }
}
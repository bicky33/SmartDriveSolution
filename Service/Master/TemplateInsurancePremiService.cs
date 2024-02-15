using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Master
{
    public class TemplateInsurancePremiService : IServiceEntityBase<TemplateInsurancePremiResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public TemplateInsurancePremiService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<TemplateInsurancePremiResponse> CreateAsync(TemplateInsurancePremiResponse entity)
        {
            var temi = entity.Adapt<TemplateInsurancePremi>();
            _repositoryManagerMaster.TemplateInsurancePremiRepository.CreateEntity(temi);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return temi.Adapt<TemplateInsurancePremiResponse>();
        }

        public async Task DeleteAsync(int id)
        {
            var temi = await _repositoryManagerMaster.TemplateInsurancePremiRepository.GetEntityById(id, false);
            if (temi == null)
            {
                throw new EntityNotFoundException(id);
            }
            _repositoryManagerMaster.TemplateInsurancePremiRepository.DeleteEntity(temi);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TemplateInsurancePremiResponse>> GetAllAsync(bool trackChanges)
        {
            var temis = await _repositoryManagerMaster.TemplateInsurancePremiRepository.GetAllEntity(false);
            var temisResponse = temis.Adapt<IEnumerable<TemplateInsurancePremiResponse>>();
            return temisResponse;
        }

        public async Task<TemplateInsurancePremiResponse> GetByIdAsync(int id, bool trackChanges)
        {
            var temi = await _repositoryManagerMaster.TemplateInsurancePremiRepository.GetEntityById(id, false);
            if (temi == null)
            {
                throw new EntityNotFoundException(id);
            }
            var temiResponse = temi.Adapt<TemplateInsurancePremiResponse>();
            return temiResponse;
        }

        public async Task<TemplateInsurancePremiResponse> UpdateAsync(int id, TemplateInsurancePremiResponse entity)
        {
            var temi = await _repositoryManagerMaster.TemplateInsurancePremiRepository.GetEntityById(id, true);
            if (temi == null)
            {
                throw new EntityNotFoundException(id);
            }
            temi.TemiId = entity.TemiId;
            temi.TemiName = entity.TemiName;
            temi.TemiRateMin = entity.TemiRateMin;
            temi.TemiRateMax = entity.TemiRateMax;
            temi.TemiNominal = entity.TemiNominal;
            temi.TemiType = entity.TemiType;
            temi.TemiZonesId = entity.TemiZonesId;
            temi.TemiIntyName = entity.TemiIntyName;
            temi.TemiCateId = entity.TemiCateId;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
            return temi.Adapt<TemplateInsurancePremiResponse>();
        }
    }
}
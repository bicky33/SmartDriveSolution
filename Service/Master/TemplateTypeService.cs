using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Master
{
    public class TemplateTypeService : IServiceEntityBase<TemplateTypeResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public TemplateTypeService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<TemplateTypeResponse> CreateAsync(TemplateTypeResponse entity)
        {
            var templateType = entity.Adapt<TemplateType>();
            _repositoryManagerMaster.TemplateTypeRepository.CreateEntity(templateType);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return templateType.Adapt<TemplateTypeResponse>();
        }

        public async Task DeleteAsync(int id)
        {
            var templateType = await _repositoryManagerMaster.TemplateTypeRepository.GetEntityById(id, false);
            if (templateType == null)
            {
                throw new EntityNotFoundException(id, nameof(templateType));
            }
            _repositoryManagerMaster.TemplateTypeRepository.DeleteEntity(templateType);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TemplateTypeResponse>> GetAllAsync(bool trackChanges)
        {
            var categories = await _repositoryManagerMaster.TemplateTypeRepository.GetAllEntity(false);
            var categoriesResponse = categories.Adapt<IEnumerable<TemplateTypeResponse>>();
            return categoriesResponse;
        }

        public async Task<TemplateTypeResponse> GetByIdAsync(int id, bool trackChanges)
        {
            var templateType = await _repositoryManagerMaster.TemplateTypeRepository.GetEntityById(id, false);
            if (templateType == null)
            {
                throw new EntityNotFoundException(id, nameof(templateType));
            }
            var templateTypeResponse = templateType.Adapt<TemplateTypeResponse>();
            return templateTypeResponse;
        }

        public async Task UpdateAsync(int id, TemplateTypeResponse entity)
        {
            var templateType = await _repositoryManagerMaster.TemplateTypeRepository.GetEntityById(id, true);
            if (templateType == null)
            {
                throw new EntityNotFoundException(id, nameof(entity));
            }
            templateType.TetyId = entity.TetyId;
            templateType.TetyName = entity.TetyName.ToString();
            templateType.TetyGroup = entity.TetyGroup.ToString();

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }
    }
}
using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;

namespace Service.Master
{
    public class TemplateTaskWorkorderService : IServiceEntityBase<TemplateTaskWorkorderResponse>
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public TemplateTaskWorkorderService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<TemplateTaskWorkorderResponse> CreateAsync(TemplateTaskWorkorderResponse entity)
        {
            var templateTaskWorkorder = entity.Adapt<TemplateTaskWorkorder>();
            _repositoryManagerMaster.TemplateTaskWorkorderRepository.CreateEntity(templateTaskWorkorder);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return templateTaskWorkorder.Adapt<TemplateTaskWorkorderResponse>();
        }

        public async Task DeleteAsync(int id)
        {
            var templateTaskWorkorder = await _repositoryManagerMaster.TemplateTaskWorkorderRepository.GetEntityById(id, false);
            if (templateTaskWorkorder == null)
            {
                throw new EntityNotFoundException(id, nameof(templateTaskWorkorder));
            }
            _repositoryManagerMaster.TemplateTaskWorkorderRepository.DeleteEntity(templateTaskWorkorder);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TemplateTaskWorkorderResponse>> GetAllAsync(bool trackChanges)
        {
            var categories = await _repositoryManagerMaster.TemplateTaskWorkorderRepository.GetAllEntity(false);
            var categoriesResponse = categories.Adapt<IEnumerable<TemplateTaskWorkorderResponse>>();
            return categoriesResponse;
        }

        public async Task<TemplateTaskWorkorderResponse> GetByIdAsync(int id, bool trackChanges)
        {
            var templateTaskWorkorder = await _repositoryManagerMaster.TemplateTaskWorkorderRepository.GetEntityById(id, false);
            if (templateTaskWorkorder == null)
            {
                throw new EntityNotFoundException(id, nameof(templateTaskWorkorder));
            }
            var templateTaskWorkorderResponse = templateTaskWorkorder.Adapt<TemplateTaskWorkorderResponse>();
            return templateTaskWorkorderResponse;
        }

        public async Task UpdateAsync(int id, TemplateTaskWorkorderResponse entity)
        {
            var templateTaskWorkorder = await _repositoryManagerMaster.TemplateTaskWorkorderRepository.GetEntityById(id, true);
            if (templateTaskWorkorder == null)
            {
                throw new EntityNotFoundException(id, nameof(entity));
            }
            templateTaskWorkorder.TewoId = entity.TewoId;
            templateTaskWorkorder.TewoName = entity.TewoName;
            templateTaskWorkorder.TewoTestaId = entity.TewoTestaId;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }
    }
}
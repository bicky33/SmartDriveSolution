using Contract.DTO.Master;
using Domain.Entities.Master;
using Domain.Exceptions;
using Domain.Repositories.Master;
using Mapster;
using Service.Abstraction.Base;
using Service.Abstraction.Master;

namespace Service.Master
{
    public class TemplateServiceTaskService : IServiceTemplateServiceTask
    {
        private readonly IRepositoryManagerMaster _repositoryManagerMaster;

        public TemplateServiceTaskService(IRepositoryManagerMaster repositoryManagerMaster)
        {
            _repositoryManagerMaster = repositoryManagerMaster;
        }

        public async Task<TemplateServiceTaskResponse> CreateAsync(TemplateServiceTaskResponse entity)
        {
            var templateServiceTask = entity.Adapt<TemplateServiceTask>();
            _repositoryManagerMaster.TemplateServiceTaskRepository.CreateEntity(templateServiceTask);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();

            return templateServiceTask.Adapt<TemplateServiceTaskResponse>();
        }

        public async Task DeleteAsync(int id)
        {
            var templateServiceTask = await _repositoryManagerMaster.TemplateServiceTaskRepository.GetEntityById(id, false);
            if (templateServiceTask == null)
            {
                throw new EntityNotFoundException(id, nameof(templateServiceTask));
            }
            _repositoryManagerMaster.TemplateServiceTaskRepository.DeleteEntity(templateServiceTask);
            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TemplateServiceTaskResponse>> GetAllAsync(bool trackChanges)
        {
            var testa = await _repositoryManagerMaster.TemplateServiceTaskRepository.GetAllEntity(false);
            var testaResponse = testa.Adapt<IEnumerable<TemplateServiceTaskResponse>>();
            return testaResponse;
        }

        public async Task<IEnumerable<TemplateServiceTaskResponse>> GetAllTestaAsync(int id, bool trackChanges)
        {
            var testa = await _repositoryManagerMaster.TemplateServiceTaskRepository.GetAllTestaByTestaTetyID(id,false);
            var testaResponse = testa.Adapt<IEnumerable<TemplateServiceTaskResponse>>();
            return testaResponse;
        }

        public async Task<TemplateServiceTaskResponse> GetByIdAsync(int id, bool trackChanges)
        {
            var templateServiceTask = await _repositoryManagerMaster.TemplateServiceTaskRepository.GetEntityById(id, false);
            if (templateServiceTask == null)
            {
                throw new EntityNotFoundException(id, nameof(templateServiceTask));
            }
            var templateServiceTaskResponse = templateServiceTask.Adapt<TemplateServiceTaskResponse>();
            return templateServiceTaskResponse;
        }


        public async Task UpdateAsync(int id, TemplateServiceTaskResponse entity)
        {
            var templateServiceTask = await _repositoryManagerMaster.TemplateServiceTaskRepository.GetEntityById(id, true);
            if (templateServiceTask == null)
            {
                throw new EntityNotFoundException(id, nameof(entity));
            }
            templateServiceTask.TestaId = entity.TestaId;
            templateServiceTask.TestaName = entity.TestaName;
            templateServiceTask.TestaGroup = entity.TestaGroup;
            templateServiceTask.TestaTetyId = entity.TestaTetyId;

            await _repositoryManagerMaster.UnitOfWork.SaveChangesAsync();
        }
    }
}
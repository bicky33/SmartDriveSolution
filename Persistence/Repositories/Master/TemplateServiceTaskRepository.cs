using Domain.Entities.Master;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class TemplateServiceTaskRepository : RepositoryBase<TemplateServiceTask>, IRepositoryTemplateServiceTask
    {
        public TemplateServiceTaskRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(TemplateServiceTask entity)
        {
            Create(entity);
        }

        public void DeleteEntity(TemplateServiceTask entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<TemplateServiceTask>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(testa => testa.TestaId).ToListAsync();
        }

        public async Task<IEnumerable<TemplateServiceTask>> GetAllTestaByTestaTetyID(int id, bool trackChanges)
        {
            return await GetAll(trackChanges).Where(testa => testa.TestaTetyId == id).ToListAsync();
        }

        public async Task<TemplateServiceTask> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(testa => testa.TestaId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<TemplateServiceTask> GetTestaByTestaTetyID(int id, bool trackChanges)
        {
            return await GetByCondition(testa => testa.TestaTetyId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}
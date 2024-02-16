using Domain.Entities.Master;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class TemplateTaskWorkorderRepository : RepositoryBase<TemplateTaskWorkorder>, IRepositoryEntityBase<TemplateTaskWorkorder>
    {
        public TemplateTaskWorkorderRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(TemplateTaskWorkorder entity)
        {
            Create(entity);
        }

        public void DeleteEntity(TemplateTaskWorkorder entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<TemplateTaskWorkorder>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(tewo => tewo.TewoId).ToListAsync();
        }

        public async Task<TemplateTaskWorkorder> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(tewo => tewo.TewoId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}
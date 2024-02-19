using Domain.Entities.Master;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class TemplateTypeRepository : RepositoryBase<TemplateType>, IRepositoryEntityBase<TemplateType>
    {
        public TemplateTypeRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(TemplateType entity)
        {
            Create(entity);
        }

        public void DeleteEntity(TemplateType entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<TemplateType>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(tety => tety.TetyId).ToListAsync();
        }

        public async Task<TemplateType> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(tety => tety.TetyId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}
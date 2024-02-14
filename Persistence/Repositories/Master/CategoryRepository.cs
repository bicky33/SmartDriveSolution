using Domain.Entities.Master;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class CategoryRepository : RepositoryBase<Category>, IRepositoryEntityBase<Category>
    {
        public CategoryRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Category entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Category entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Category>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(cat => cat.CateId).ToListAsync();
        }

        public async Task<Category> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(cb => cb.CateId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}
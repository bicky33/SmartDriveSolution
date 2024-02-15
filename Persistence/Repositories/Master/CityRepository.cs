using Domain.Entities.Master;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class CityRepository : RepositoryBase<City>, IRepositoryEntityBase<City>
    {
        public CityRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(City entity)
        {
            Create(entity);
        }

        public void DeleteEntity(City entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<City>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(c => c.CityId).ToListAsync();
        }

        public async Task<City> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.CityId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}
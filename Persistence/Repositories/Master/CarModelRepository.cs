using Domain.Entities.Master;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class CarModelRepository : RepositoryBase<CarModel>, IRepositoryEntityBase<CarModel>
    {
        public CarModelRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(CarModel entity)
        {
            Create(entity);
        }

        public void DeleteEntity(CarModel entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<CarModel>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(cm => cm.CarmId).ToListAsync();
        }

        public async Task<CarModel> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(cm => cm.CarmId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}
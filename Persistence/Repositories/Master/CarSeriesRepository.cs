using Domain.Entities.Master;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class CarSeriesRepository : RepositoryBase<CarSeries>, IRepositoryEntityBase<CarSeries>
    {
        public CarSeriesRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(CarSeries entity)
        {
            Create(entity);
        }

        public void DeleteEntity(CarSeries entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<CarSeries>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(cs => cs.CarsId).ToListAsync();
        }

        public async Task<CarSeries> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(cs => cs.CarsId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}
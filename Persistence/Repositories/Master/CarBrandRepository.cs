using Domain.Entities.Master;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class CarBrandRepository : RepositoryBase<CarBrand>, IRepositoryEntityBase<CarBrand>
    {
        public CarBrandRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(CarBrand entity)
        {
            Create(entity);
        }

        public void DeleteEntity(CarBrand entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<CarBrand>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(cb => cb.CabrId).ToListAsync();
        }

        public async Task<CarBrand> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(cb => cb.CabrId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}
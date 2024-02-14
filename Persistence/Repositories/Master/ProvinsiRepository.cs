using Domain.Entities.Master;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class ProvinsiRepository : RepositoryBase<Provinsi>, IRepositoryEntityBase<Provinsi>
    {
        public ProvinsiRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Provinsi entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Provinsi entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<Provinsi>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(prov => prov.ProvId).ToListAsync();
        }

        public async Task<Provinsi> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(prov => prov.ProvId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}
using Domain.Entities.Payment;
using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Payment
{
    public class FintechRepository : RepositoryBase<Fintech>, IRepositoryEntityBase<Fintech>
    {
        public FintechRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(Fintech entity)
        {
            Create(entity);
        }

        public void DeleteEntity(Fintech entity)
        {
            Delete(entity);

        }

        public async Task<IEnumerable<Fintech>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x => x.FintEntityid).ToListAsync();

        }

        public async Task<Fintech> GetEntityById(int? id, bool trackChanges)
        {
            return await GetByCondition(x => x.FintEntityid.Equals(id), trackChanges).SingleOrDefaultAsync();

        }
    }
}

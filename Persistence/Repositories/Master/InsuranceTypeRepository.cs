using Domain.Entities.Master;
using Domain.Repositories.Master;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;

namespace Persistence.Repositories.Master
{
    public class InsuranceTypeRepository : RepositoryBase<InsuranceType>, IRepositoryEntityBaseMaster<InsuranceType>
    {
        public InsuranceTypeRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntityMaster(InsuranceType entity)
        {
            Create(entity);
        }

        public void DeleteEntityMaster(InsuranceType entity)
        {
            Delete(entity);
        }

        public async Task<IEnumerable<InsuranceType>> GetAllEntityMaster(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(it => it.IntyName).ToListAsync();
        }

        public async Task<InsuranceType> GetEntityByNameMaster(string name, bool trackChanges)
        {
            return await GetByCondition(it => it.IntyName.Equals(name), trackChanges).SingleOrDefaultAsync();
        }
    }
}